using Application.Core;
using Application.DTOs.FoodCatalogDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;

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
                _context.FoodCatalogsDb.Add(foodCatalog);

                await _context.SaveChangesAsync();

                var foodCatalogDto = _mapper.Map<FoodCatalogPostDTO>(foodCatalog);
                return Result<FoodCatalogPostDTO>.Success(foodCatalogDto); // Zwraca DTO z uzupełnionym ID
            }

        }
    }
}
