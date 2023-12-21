using Application.Core;
using Application.DTOs.GenericsDTO;
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
                        DietName = d.Name,
                        CreateTime = d.dateAdded,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                        
                        Period = (int)(d.EndDate - d.StartDate).TotalDays
                    })
                    .ToListAsync();

                if (dietSales == null || dietSales.Count == 0)
                {
                    return Result<List<DietSalesDTO>>.Failure("cannot create diet sales.");
                }

                return Result<List<DietSalesDTO>>.Success(dietSales);
            }
        }
    }
}
