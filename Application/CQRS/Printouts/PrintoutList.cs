using Application.Core;
using Application.DTOs.PrintoutsDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Printouts
{
    public class PrintoutList
    {
        public class Query : IRequest<Result<List<ParameterizedPrintoutGetDTO>>>
        {
            public class Handler : IRequestHandler<Query, Result<List<ParameterizedPrintoutGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<ParameterizedPrintoutGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var printoutTemplate = await _context.PrintoutsDb
                        .Select(m => new ParameterizedPrintoutGetDTO
                        {
                            Id = m.Id,
                            Name = m.Name,
                            Description = m.Description,
                        })
                        .ToListAsync(cancellationToken);

                        return Result<List<ParameterizedPrintoutGetDTO>>.Success(printoutTemplate);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<ParameterizedPrintoutGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}