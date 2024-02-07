using Application.Core;
using Application.DTOs.BillDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Bills
{
    public class BillsPatientList
    {
        public class Query : IRequest<Result<List<DietSalesBillGetDTO>>>
        {
            public int PatientId { get; set; }
            public class Handler : IRequestHandler<Query, Result<List<DietSalesBillGetDTO>>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<List<DietSalesBillGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var billsList = await _context.DietSalesBillsDb
                            .Include(b => b.Sales)
                            .Where(b => b.PatientId == request.PatientId)
                            .ToListAsync(cancellationToken);

                        var billsListDto = new List<DietSalesBillGetDTO>();

                        foreach (var bill in billsList)
                        {
                            var dietician = await _context.DieticiansDb.FirstOrDefaultAsync(d => d.Id == bill.DieticianId, cancellationToken);
                            var billDto = new DietSalesBillGetDTO
                            {
                                Id = bill.Id,
                                DieticianId = bill.DieticianId,
                                PatientId = bill.PatientId,
                                SalesId = bill.SalesId,
                                Sales = bill.Sales != null ? new SalesGetDTO
                                {
                                    Id = bill.Sales.Id,
                                    DietId = bill.Sales.DietId,
                                    Price = bill.Sales.Price,
                                    IsPaid = bill.Sales.IsPaid,
                                    SalesDate = bill.Sales.SalesDate.ToShortDateString(),
                                } : null,
                                DieticianName = dietician != null ? dietician.FirstName + " " + dietician.LastName : null
                            };

                            billsListDto.Add(billDto);
                        }

                        return Result<List<DietSalesBillGetDTO>>.Success(billsListDto);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<DietSalesBillGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }

                //public async Task<Result<List<DietSalesBillGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                //{
                //    try
                //    {
                //        var billsList = await _context.DietSalesBillsDb
                //            .Include(b => b.Sales)
                //        .Where(b => b.PatientId == request.PatientId)
                //        .ToListAsync(cancellationToken);

                //        var billsListDto = _mapper.Map<List<DietSalesBillGetDTO>>(billsList);

                //        return Result<List<DietSalesBillGetDTO>>.Success(billsListDto);
                //    }
                //    catch (Exception ex)
                //    {
                //        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                //        return Result<List<DietSalesBillGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                //    }
                //}
            }
        }
    }
}
