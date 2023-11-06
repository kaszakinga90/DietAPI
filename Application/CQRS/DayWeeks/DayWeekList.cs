using Application.DTOs.DayWeekDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.CQRS.DayWeeks
{
    public class DayWeekList
    {
        public class Query : IRequest<List<DayWeekDTO>> { }

        public class Handler : IRequestHandler<Query, List<DayWeekDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<DayWeekDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dayWeeks = await _context.DayWeeksDb.ToListAsync(cancellationToken);
                return _mapper.Map<List<DayWeekDTO>>(dayWeeks);
            }
        }
    }
}