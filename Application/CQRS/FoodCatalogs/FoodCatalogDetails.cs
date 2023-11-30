using Application.DTOs.FoodCatalogDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.FoodCatalogs
{
    public class FoodCatalogDetails
    {
        public class Query : IRequest<FoodCatalogGetDTO>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, FoodCatalogGetDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FoodCatalogGetDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var foodCatalog = await _context.FoodCatalogsDb.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                return _mapper.Map<FoodCatalogGetDTO>(foodCatalog);
            }
        }
    }
}
