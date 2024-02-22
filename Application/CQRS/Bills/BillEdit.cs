using Application.Core;
using Application.DTOs.BillDTO;
using Application.Validators.Bill;
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
            private readonly BillUpdateValidator _validator;

            public Handler(DietContext context, IMapper mapper, BillUpdateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<SalesPutDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.SalesPutDTO);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<SalesPutDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

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
                    var result = await _context.SaveChangesAsync() > 0;
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