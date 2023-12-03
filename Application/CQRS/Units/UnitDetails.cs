using Application.DTOs.UnitDTO;
using AutoMapper;
using DietDB;
using MediatR;
namespace Application.CQRS.Units
{
    public class UnitDetails
    {
        public class Query : IRequest<UnitGetDTO>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, UnitGetDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UnitGetDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var unit = await _context.UnitsDb.FindAsync(request.Id);
                return _mapper.Map<UnitGetDTO>(unit);
            }
        }
    }
}
