﻿using Application.Core;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Admins
{
    public class AdminMessageList
    {
        public class Query : IRequest<Result<PagedList<MessageToDTO>>>
        {
            public int AdminId { get; set; }
            public PagingParams Params { get; set; }
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
                try
                {
                    var adminMessagesList = _context.MessageToDb
                    .Where(m => m.AdminId == request.AdminId && m.AdminSended==false)
                    .Select(m => new MessageToDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        DieticianId = m.DieticianId,
                        AdminId = m.AdminId,
                        DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                        AdminName = m.Admin.FirstName + " " + m.Admin.LastName,
                        PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                        dateAdded = m.dateAdded,
                        ReadDate = m.ReadDate,
                        IsRead = m.IsRead,
                        PatientSended = m.PatientSended,
                        DieticianSended = m.DieticianSended,
                        AdminSended = m.AdminSended,
                    })
                    .AsQueryable();

                    return Result<PagedList<MessageToDTO>>.Success(
                        await PagedList<MessageToDTO>.CreateAsync(adminMessagesList, request.Params.PageNumber, request.Params.PageSize)
                        );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PagedList<MessageToDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}