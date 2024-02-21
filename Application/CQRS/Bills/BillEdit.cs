using Application.Core;
using Application.DTOs.BillDTO;
using AutoMapper;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Bills
{
    public class BillEdit
    {
        public class Command : IRequest<Result<SalesPutDTO>>
        {
            public SalesPutDTO SalesPutDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SalesPutDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<SalesPutDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var salesDto = request.SalesPutDTO;

                var sales = await _context.SalesDb.FindAsync(salesDto.Id);

                if (sales == null)
                {
                    return Result<SalesPutDTO>.Failure("Nie znaleziono rachunku o podanym id.");
                }

                _mapper.Map(request.SalesPutDTO, sales);
                sales.IsPaid = true;

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<SalesPutDTO>.Failure("Opłacenie rachunku nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<SalesPutDTO>.Failure("Wystąpił błąd podczas opłacania rachunku.");
                }
                return Result<SalesPutDTO>.Success(_mapper.Map<SalesPutDTO>(sales));
            }
        }
    }
}
