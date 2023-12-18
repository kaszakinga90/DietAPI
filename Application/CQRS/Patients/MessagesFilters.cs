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
            public class Query : IRequest<Result<PatientMessagesFiltersDTO>>
            {
                public int PatientId { get; set; }
            }

            public class Handler : IRequestHandler<Query, Result<PatientMessagesFiltersDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<PatientMessagesFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var filters = new PatientMessagesFiltersDTO
                    {
                        DatesAdded = await _context.MessageToDb
                            .Where(m => m.PatientId == request.PatientId && m.DieticianId != null)
                            .Select(m => m.dateAdded)
                            .Distinct()
                            .ToListAsync(),

                        DieticianNames = await _context.MessageToDb
                            .Where(m => m.PatientId == request.PatientId && m.DieticianId != null)
                            .Select(m => m.Dietician.FirstName + " " + m.Dietician.LastName)
                            .Distinct()
                            .ToListAsync()
                    };
                    return Result<PatientMessagesFiltersDTO>.Success(filters);
                }
            }
        }
    }
}
