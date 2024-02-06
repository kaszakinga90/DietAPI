using Application.Core;
using Application.DTOs.BillDTO;
using Application.DTOs.InvitationDTO;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Bills
{
    public class BillEdit
    {
        public class Command : IRequest<Result<DietSalesBillPutDTO>>
        {
            public DietSalesBillPutDTO DietSalesBillPutDTO { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<DietSalesBillPutDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DietSalesBillPutDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var billDto = request.DietSalesBillPutDTO;

                var dietSalesBill = await _context.DietSalesBillsDb.FindAsync(billDto.Id);

                if (dietSalesBill == null)
                {
                    return Result<DietSalesBillPutDTO>.Failure("Nie znaleziono rachunku o podanym id.");
                }

                _mapper.Map(request.DietSalesBillPutDTO, dietSalesBill);
                dietSalesBill.Sales.IsPaid = true;

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<DietSalesBillPutDTO>.Failure("Opłacenie rachunku nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DietSalesBillPutDTO>.Failure("Wystąpił błąd podczas opłacania rachunku.");
                }
                return Result<DietSalesBillPutDTO>.Success(_mapper.Map<DietSalesBillPutDTO>(dietSalesBill));
            }
        }
    }
}
