using Application.Core;
using Application.DTOs.BillDTO;
using Application.Validators.FoodCatalog;
using AutoMapper;
using DietDB;
using MediatR;
using ModelsDB;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Bills
{
    public class BillCreate
    {
        public class Command : IRequest<Result<DietSalesBillPostDTO>>
        {
            public DietSalesBillPostDTO DietSalesBillPostDTO { get; set; }
        }
        public class Hendler : IRequestHandler<Command, Result<DietSalesBillPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly FoodCatalogDieticianCreateValidator _validator;

            public Hendler(DietContext context, IMapper mapper, FoodCatalogDieticianCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<DietSalesBillPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                //var validationResult = await _validator
                //    .ValidateAsync(request.DietSalesBillPostDTO, cancellationToken);

                //if (!validationResult.IsValid)
                //{
                //    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                //    return Result<DietSalesBillPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                //}

                var dietSales = _mapper.Map<DietSalesBill>(request.DietSalesBillPostDTO);

                if (dietSales == null)
                {
                    return Result<DietSalesBillPostDTO>.Failure("Niepowodzenie mapowania.");
                }

                _context.DietSalesBillsDb.Add(dietSales);

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<DietSalesBillPostDTO>.Failure("Operacja nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DietSalesBillPostDTO>.Failure("Wystąpił błąd podczas dodawania food catalog.");
                }

                return Result<DietSalesBillPostDTO>.Success(_mapper.Map<DietSalesBillPostDTO>(dietSales));
            }
        }
    }
}
