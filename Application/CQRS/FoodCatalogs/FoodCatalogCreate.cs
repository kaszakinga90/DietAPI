using Application.Core;
using Application.DTOs.FoodCatalogDTO;
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

            public Hendler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<FoodCatalogPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
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