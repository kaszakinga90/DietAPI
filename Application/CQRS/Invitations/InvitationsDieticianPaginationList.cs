﻿using Application.Core;
using Application.DTOs.InvitationDTO;
using Application.FiltersExtensions.Invitations;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Invitations
{
    public class InvitationsDieticianPaginationList
    {
        public class Query : IRequest<Result<PagedList<InvitationGetDTO>>>
        {
            public int DieticianId { get; set; }
            public InvitationParams Params { get; set; }

            public class Handler : IRequestHandler<Query, Result<PagedList<InvitationGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<PagedList<InvitationGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var invitationsList = _context.InvitationsDb
                        .Where(m => m.DieticianId == request.DieticianId)
                        .Select(m => new InvitationGetDTO
                        {
                            Id = m.Id,
                            DieticianId = m.DieticianId,
                            PatientId = m.PatientId,
                            IsSended = m.IsSended,
                            IsAccepted = m.IsAccepted,
                            IsDeclined = m.IsDeclined,
                            PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                        })
                        .AsQueryable();

                        return Result<PagedList<InvitationGetDTO>>.Success(
                                await PagedList<InvitationGetDTO>.CreateAsync(invitationsList, request.Params.PageNumber, request.Params.PageSize)
                                );
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<PagedList<InvitationGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}