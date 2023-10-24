using Application.DTOs.DayWeekDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.CQRS.DayWeekDTOs
{
    public class DayWeekDetails
    {
        public class Query : IRequest<DayWeekDTO>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DayWeekDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<DayWeekDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var dayWeek = await _context.DayWeek.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken); ;
                return _mapper.Map<DayWeekDTO>(dayWeek);
            }
        }
    }
}
