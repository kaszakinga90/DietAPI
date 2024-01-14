using Application.Core;
using Application.DTOs.PatientCardDTO;
using Application.FiltersExtensions.PatientMessages;
using Application.FiltersExtensions.PatientsCards;
using DietDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.PatientCards
{
    public class PatientCardList
    {
        public class Query : IRequest<Result<PagedList<PatientCardGetDTO>>>
        {
            public int DieticianId { get; set; }
            public PatientCardParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PatientCardGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<PatientCardGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patientCardList = _context.PatientCardsDb
                    .Where(m => m.DieticianId == request.DieticianId)
                    .Select(m => new PatientCardGetDTO
                    {
                        Id = m.Id,
                        PatientId=m.PatientId,
                        PatientName=m.Patient.FirstName+" "+m.Patient.LastName,
                    })
                    .AsQueryable();
                patientCardList = patientCardList.Search(request.Params.SearchTerm);
                return Result<PagedList<PatientCardGetDTO>>.Success(
                    await PagedList<PatientCardGetDTO>.CreateAsync(patientCardList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}