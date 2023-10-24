using Application.Core;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.CQRS.PatientDTOs
{
    public class PatientMessageList
    {
        public class Query : IRequest<Result<PagedList<MessageToPatientDTO>>>
        {
            public int PatientId { get; set; }
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<MessageToPatientDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<MessageToPatientDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patientMessagesList = _context.MessageToPatients
                    .Where(m => m.PatientId == request.PatientId)
                    .Select(m => new MessageToPatientDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        DieticianId = m.DieticianId,
                        PatientId = m.PatientId,
                        DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName
                    })
                    .AsQueryable();
                return Result<PagedList<MessageToPatientDTO>>.Success(
                    await PagedList<MessageToPatientDTO>.CreateAsync(patientMessagesList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}

