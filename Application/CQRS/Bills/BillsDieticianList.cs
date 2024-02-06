using Application.Core;
using Application.DTOs.BillDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Bills
{
    public class BillsDieticianList
    {
        public class Query : IRequest<Result<List<DietSalesBillGetDTO>>>
        {
            public int DieticianId { get; set; }
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
                        .Where(b => b.DieticianId == request.DieticianId)
                        .ToListAsync(cancellationToken);

                        var billsListDto = _mapper.Map<List<DietSalesBillGetDTO>>(billsList);

                        return Result<List<DietSalesBillGetDTO>>.Success(billsListDto);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<DietSalesBillGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}
