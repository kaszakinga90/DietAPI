using Application.Core;
using Application.DTOs.AdminDTO;
using Application.DTOs.FoodCatalogDTO;
using Application.Validators.FoodCatalog;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using System.Diagnostics;

namespace Application.CQRS.FoodCatalogs
{
    public class FoodCatalogCreate
    {
        public class Command : IRequest<Result<FoodCatalogPostDTO>>
        {
            public FoodCatalogPostDTO FoodCatalogPostDTO { get; set; }
        }
        public class Hendler : IRequestHandler<Command, Result<FoodCatalogPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly FoodCatalogDieticianCreateValidator _validator;

            public Hendler(DietContext context, IMapper mapper, FoodCatalogDieticianCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<FoodCatalogPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.FoodCatalogPostDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<FoodCatalogPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var foodCatalog = _mapper.Map<FoodCatalog>(request.FoodCatalogPostDTO);

                if (foodCatalog == null)
                {
                    return Result<FoodCatalogPostDTO>.Failure("Niepowodzenie mapowania.");
                }

                _context.FoodCatalogsDb.Add(foodCatalog);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<FoodCatalogPostDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<FoodCatalogPostDTO>.Failure("Wystąpił błąd podczas dodawania food catalog.");
                }

                return Result<FoodCatalogPostDTO>.Success(_mapper.Map<FoodCatalogPostDTO>(foodCatalog));
            }
        }
    }
}