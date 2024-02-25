using Application.Core;
using Application.DTOs.DiplomaDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Diplomas
{
    public class DiplomasDieticianList
    {
        public class Query : IRequest<Result<List<DiplomaGetDTO>>>
        {
            public int DieticianId { get; set; }

            public class Handler : IRequestHandler<Query, Result<List<DiplomaGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<DiplomaGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var diplomaList = await _context.DiplomasDb
                        .Where(m => m.DieticianId == request.DieticianId && m.isActive)
                        .Select(m => new DiplomaGetDTO
                        {
                            Id = m.Id,
                            Title = m.Title,
                            Description = m.Description,
                            PictureUrl = m.PictureUrl,
                        })
                        .ToListAsync(cancellationToken);

                        return Result<List<DiplomaGetDTO>>.Success(diplomaList);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<DiplomaGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}