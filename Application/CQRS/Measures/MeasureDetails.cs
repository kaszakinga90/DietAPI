using Application.DTOs.MeasureDTO;
using AutoMapper;
using DietDB;
using MediatR;

namespace Application.CQRS.Measures
{
    public class MeasureDetails
    {
        public class Query : IRequest<MeasureGetDTO>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, MeasureGetDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<MeasureGetDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var measure = await _context.MeasuresDb.FindAsync(request.Id);
                return _mapper.Map<MeasureGetDTO>(measure);
            }
        }
    }
}
