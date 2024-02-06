using Application.Core;
using Application.DTOs.BillDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Bills
{
    public class BillDetails
    {
        public class Query : IRequest<Result<DietSalesBillGetDTO>>
        {
            public int DietSalesBillId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DietSalesBillGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DietSalesBillGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dietSalesBill = await _context.DietSalesBillsDb
                        .Include(bill => bill.Sales)
                        .FirstOrDefaultAsync(bill => bill.Id == request.DietSalesBillId, cancellationToken);

                    if (dietSalesBill == null)
                    {
                        return Result<DietSalesBillGetDTO>.Failure("Nie znaleziono rachunku o podanym id.");
                    }

                    var dietSalesBillGetDTO = _mapper.Map<DietSalesBillGetDTO>(dietSalesBill);
                    return Result<DietSalesBillGetDTO>.Success(dietSalesBillGetDTO);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DietSalesBillGetDTO>.Failure("Wystąpił błąd podczas pobierania szczegółów rachunku.");
                }
            }
        }
    }
}
