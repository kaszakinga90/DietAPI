using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using Application.Validators.FoodCatalog;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.FoodCatalogs
{
    public class FoodCatalogDieticianEdit
    {
        public class Command : IRequest<Result<FoodCatalogDieticianEditDTO>>
        {
            public FoodCatalogDieticianEditDTO FoodCatalogDieticianEditDTO { get; set; }

            public class Handler : IRequestHandler<Command, Result<FoodCatalogDieticianEditDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;
                private readonly FoodCatalogDieticianUpdateValidator _validator;

                public Handler(DietContext context, IMapper mapper, FoodCatalogDieticianUpdateValidator validator)
                {
                    _context = context;
                    _mapper = mapper;
                    _validator = validator;
                }

                public async Task<Result<FoodCatalogDieticianEditDTO>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var validationResult = await _validator
                    .ValidateAsync(request.FoodCatalogDieticianEditDTO, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<FoodCatalogDieticianEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }

                    var foodCatalog = await _context.FoodCatalogsDb
                        .Where(fc => fc.Id == request.FoodCatalogDieticianEditDTO.Id &&
                                fc.DieticianId == request.FoodCatalogDieticianEditDTO.DieteticianId)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (foodCatalog == null)
                    {
                        return Result<FoodCatalogDieticianEditDTO>.Failure("Food catalog o podanym ID z takim dieticianId nie został znaleziony.");
                    }

                    _mapper.Map(request.FoodCatalogDieticianEditDTO, foodCatalog);

                    try
                    {
                        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                        if (!result)
                        {
                            return Result<FoodCatalogDieticianEditDTO>.Failure("Edycja pacjenta nie powiodła się.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<FoodCatalogDieticianEditDTO>.Failure("Wystąpił błąd podczas edycji pacjenta. " + ex);
                    }
                    return Result<FoodCatalogDieticianEditDTO>.Success(_mapper.Map<FoodCatalogDieticianEditDTO>(foodCatalog));
                }
            }
        }
    }
}