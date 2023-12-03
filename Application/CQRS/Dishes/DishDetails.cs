using Application.DTOs.DishDTO;
using AutoMapper;
using DietDB;
using MediatR;

namespace Application.CQRS.Dishes
{
    public class DishDetails
    {
        public class Query : IRequest<DishGetDTO>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DishGetDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<DishGetDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var dish = await _context.DishesDb.FindAsync(request.Id);
                return _mapper.Map<DishGetDTO>(dish);
            }
        }
    }
}
