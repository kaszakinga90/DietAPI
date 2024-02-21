using Application.Core;
using Application.Validators.Ingredients;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// TODO : validacja
namespace Application.CQRS.Ingredients
{
    public class IngredientEdit
    {
        public class Command : IRequest<Result<IngredientEditDTO>>
        {
            public IngredientEditDTO IngredientEditDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<IngredientEditDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly IngredientUpdateValidator _validator;

            public Handler(DietContext context, IMapper mapper, IngredientUpdateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<IngredientEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                //var validationResult = await _validator
                //    .ValidateAsync(request.IngredientEditDTO, cancellationToken);

                //if (!validationResult.IsValid)
                //{
                //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                //    return Result<IngredientEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                //}

                var ingredient = await _context.IngridientsDb
                                    .Include(i => i.Nutrients)
                                    .SingleOrDefaultAsync(i => i.Id == request.IngredientEditDTO.Id, cancellationToken);

                if (ingredient == null)
                {
                    return Result<IngredientEditDTO>.Failure("Produkt o podanym ID nie został znaleziony.");
                }

                var relations = _context.DishIngredientsDb.Any(di => di.IngredientId == request.IngredientEditDTO.Id);

                if (relations)
                {
                    return Result<IngredientEditDTO>.Failure("Składnik jest uzywanyw innej tabeli. Nie mozna edytować.");
                }

                _mapper.Map(request.IngredientEditDTO, ingredient);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<IngredientEditDTO>.Failure("Edycja ingredient nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenie: " + ex);
                    return Result<IngredientEditDTO>.Failure("Wystąpił błąd podczas edycji ingredient. " + ex);
                }
                return Result<IngredientEditDTO>.Success(_mapper.Map<IngredientEditDTO>(ingredient));
            }
        }
    }
}