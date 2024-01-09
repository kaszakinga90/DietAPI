using Application.Core;
using Application.DTOs.MeasureDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Measures
{
    public class MeasureDetails
    {
        public class Query : IRequest<Result<MeasureGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<MeasureGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<MeasureGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var measure = await _context.MeasuresDb
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (measure == null)
                {
                    return Result<MeasureGetDTO>.Failure("Measure o podanym id nie został odnaleziony");
                }

                return Result<MeasureGetDTO>.Success(_mapper.Map<MeasureGetDTO>(measure));
            }
        }
    }
}
