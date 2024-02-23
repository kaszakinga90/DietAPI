using Application.Core;
using Application.DTOs.ReportsClassesDTO.Reports;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.DietSaleses
{
    public class DietSalesCreateDetails
    {
        public class Command : IRequest<Result<List<DietSalesDTO>>>
        {
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<List<DietSalesDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DietSalesDTO>>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dietSales = await _context.DietsDb
                    .Where(m => m.DieteticianId == request.DieticianId)
                    .Include(diet => diet.Patient)
                    .Select(d => new DietSalesDTO
                    {
                        Id = d.Id,
                        DietName = d.Name,
                        CreateTime = d.dateAdded,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                        
                        Period = (int)(d.EndDate - d.StartDate).TotalDays,
                        Price = _context.SalesDb
                .Where(s => s.DietId == d.Id)
                .Select(s => s.Price.ToString())
                .FirstOrDefault() ?? "Brak rachunku"
                    })
                    .ToListAsync();

                if (dietSales == null || dietSales.Count == 0)
                {
                    return Result<List<DietSalesDTO>>.Failure("Nie można stworzyć raportu sprzedazy.");
                }

                return Result<List<DietSalesDTO>>.Success(dietSales);
            }
        }
    }
}
