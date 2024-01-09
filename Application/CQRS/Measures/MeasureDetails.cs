using Application.Core;
using Application.DTOs.MeasureDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                try
                {
                    var measure = await _context.MeasuresDb
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    if (measure == null)
                    {
                        return Result<MeasureGetDTO>.Failure("Measure o podanym id nie został odnaleziony");
                    }

                    return Result<MeasureGetDTO>.Success(_mapper.Map<MeasureGetDTO>(measure));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<MeasureGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}