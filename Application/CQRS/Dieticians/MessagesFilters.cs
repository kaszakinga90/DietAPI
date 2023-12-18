using Application.Core;
using Application.DTOs.MessagesDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Dieticians
{
    public class MessagesFilters
    {
        public class FilterList
        {
            public class Query : IRequest<Result<DieticianMessagesFiltersDTO>>
            {
                public int DieticianId { get; set; }
            }

            public class Handler : IRequestHandler<Query, Result<DieticianMessagesFiltersDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<DieticianMessagesFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var filters = new DieticianMessagesFiltersDTO
                    {
                        DatesAdded = await _context.MessageToDb
                            .Where(m => m.DieticianId == request.DieticianId && m.PatientId != null)
                            .Select(m => m.dateAdded)
                            .Distinct()
                            .ToListAsync(),

                        PatientNames = await _context.MessageToDb
                            .Where(m => m.DieticianId == request.DieticianId && m.PatientId != null)
                            .Select(m => m.Patient.FirstName + " " + m.Patient.LastName)
                            .Distinct()
                            .ToListAsync()
                    };
                    return Result<DieticianMessagesFiltersDTO>.Success(filters);
                }
            }
        }
    }
}
