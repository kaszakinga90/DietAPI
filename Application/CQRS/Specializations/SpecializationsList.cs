using Application.Core;
using Application.DTOs.SpecializationDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Specializations
{
    public class SpecializationsList
    {
        public class Query : IRequest<Result<List<SpecializationGetDTO>>>
        {
            public class Handler : IRequestHandler<Query, Result<List<SpecializationGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<SpecializationGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var specializationsList = await _context.SpecializationsDb
                            .Where(s=>s.isActive)
                        .Select(m => new SpecializationGetDTO
                        {
                            Id = m.Id,
                            SpecializationName = m.SpecializationName
                        })
                        .ToListAsync(cancellationToken);

                        return Result<List<SpecializationGetDTO>>.Success(specializationsList);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<SpecializationGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}