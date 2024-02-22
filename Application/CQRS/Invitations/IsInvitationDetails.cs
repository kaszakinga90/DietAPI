using Application.Core;
using Application.DTOs.InvitationDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Invitations
{
    public class IsInvitationDetails
    {
        public class Query : IRequest<Result<InvitationGetDTO>>
        {
            public int PatientId { get; set; }
            public int DieticianId { get; set; }
            public class Handler : IRequestHandler<Query, Result<InvitationGetDTO>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<InvitationGetDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var invitation = await _context.InvitationsDb
                            .Where(m => m.PatientId == request.PatientId && m.DieticianId == request.DieticianId)
                            .Select(m => new InvitationGetDTO
                            {
                                IsSended = m.IsSended,
                                // Możesz dodać inne wymagane pola tutaj
                            })
                            .FirstOrDefaultAsync(cancellationToken);

                        if (invitation == null)
                        {
                            // Zwróć Result<InvitationGetDTO> z wartością IsSended ustawioną na false
                            return Result<InvitationGetDTO>.Success(new InvitationGetDTO { IsSended = false });
                        }

                        return Result<InvitationGetDTO>.Success(invitation);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<InvitationGetDTO>.Failure("Wystąpił błąd podczas pobierania danych.");
                    }
                }

            }
        }
    }
}