using Application.Core;
using Application.DTOs.MessagesDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Patients
{
    public class MessagesFilters
    {
        public class FilterList
        {
            public class Query : IRequest<Result<MessagesFiltersDTO>>
            {
                public int PatientId { get; set; }
            }

            public class Handler : IRequestHandler<Query, Result<MessagesFiltersDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<MessagesFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var filters = new MessagesFiltersDTO
                    {
                        DatesAdded = await _context.MessageToDb
                            .Where(m => m.PatientId == request.PatientId && m.DieticianId!=null)
                            .Select(m => m.dateAdded)
                            .Distinct()
                            .ToListAsync(),

                        DieticianNames = await _context.MessageToDb
                            .Where(m => m.PatientId == request.PatientId && m.DieticianId!=null)
                            .Select(m => m.Dietician.FirstName + " " + m.Dietician.LastName)
                            .Distinct()
                            .ToListAsync()
                    };
                    return Result<MessagesFiltersDTO>.Success(filters);
                }
            }
        }
    }
}
