using Application.Core;
using Application.DTOs.InvitationDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Invitations
{
    public class InvitationsPatientList
    {
        public class Query : IRequest<Result<List<InvitationGetDTO>>>
        {
            public int PatientId { get; set; }
            public class Handler : IRequestHandler<Query, Result<List<InvitationGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<InvitationGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var invitationsList = await _context.InvitationsDb
                        .Where(m => m.PatientId == request.PatientId)
                        .Select(m => new InvitationGetDTO
                        {
                            DieticianId = m.DieticianId,
                            PatientId = m.PatientId,
                            IsSended = m.IsSended,
                            IsAccepted = m.IsAccepted,
                            IsDeclined = m.IsDeclined,
                            PatientName = m.Patient.FirstName + " " + m.Patient.LastName,
                        })
                        .ToListAsync(cancellationToken);

                        return Result<List<InvitationGetDTO>>.Success(invitationsList);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<InvitationGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}