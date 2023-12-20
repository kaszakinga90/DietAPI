using Application.Core;
using Application.DTOs.AddressDTO;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.DTOs.OfficeDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.DieticiansBusinessesCards
{
    public class DieticianBusinessCardDetails
    {
        public class Query : IRequest<Result<DieticianBusinessCardGetDTO>>
        {
            public int DieticianId { get; set; }

            public class Handler : IRequestHandler<Query, Result<DieticianBusinessCardGetDTO>>
            {
                private readonly DietContext _context;
                private readonly IMapper _mapper;

                public Handler(DietContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<Result<DieticianBusinessCardGetDTO>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var dietician = await _context.DieticiansDb
                        .Include(d => d.DieticianOffices)
                            .ThenInclude(o => o.Office)
                                .ThenInclude(a => a.Address)
                        .Include(d => d.Diplomas)
                        .Include(d => d.DieticianSpecializations)
                                .ThenInclude(s=>s.Specialization)
                        .Include(d => d.Logo)
                        .FirstOrDefaultAsync(d => d.Id == request.DieticianId);

                    if (dietician == null)
                    {
                        return Result<DieticianBusinessCardGetDTO>.Failure("Dietician not found.");
                    }

                    var dieticianBusinessCardDTO = _mapper.Map<DieticianBusinessCardGetDTO>(dietician);

                    return Result<DieticianBusinessCardGetDTO>.Success(dieticianBusinessCardDTO);
                }
            }
        }
    }
}