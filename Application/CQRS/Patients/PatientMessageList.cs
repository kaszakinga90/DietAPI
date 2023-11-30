﻿using Application.Core;
using Application.FiltersExtensions.PatientMessages;
using DietDB;
using MediatR;

namespace Application.CQRS.Patients
{
    public class PatientMessageList
    {
        public class Query : IRequest<Result<PagedList<MessageToDTO>>>
        {
            public int PatientId { get; set; }
            public PatientMessagesParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<MessageToDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<PagedList<MessageToDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patientMessagesList = _context.MessageToDb
                    .Where(m => m.PatientId == request.PatientId && m.AdminId==null)
                    .Select(m => new MessageToDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        DieticianId = m.DieticianId,
                        PatientId = m.PatientId,
                        DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                        AdminName = m.Admin.FirstName + " " + m.Admin.LastName,
                        PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                        dateAdded=m.dateAdded,
                        ReadDate=m.ReadDate,
                        IsRead=m.IsRead
                    })
                    .AsQueryable();
                patientMessagesList = patientMessagesList.Sort(request.Params.OrderBy);
                patientMessagesList = patientMessagesList.Filter(request.Params.DieticianNames);
                patientMessagesList = patientMessagesList.Search(request.Params.SearchTerm);
                return Result<PagedList<MessageToDTO>>.Success(
                    await PagedList<MessageToDTO>.CreateAsync(patientMessagesList, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}

