using Application.Core;
using Application.DTOs.DieticianBusinessCardDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                    try
                    {
                        var dietician = await _context.DieticiansDb
                        .Include(d => d.DieticianOffices)
                            .ThenInclude(o => o.Office)
                                .ThenInclude(a => a.Address)
                        .Include(d => d.Diplomas)
                        .Include(d => d.DieticianSpecializations)
                                .ThenInclude(s => s.Specialization)
                        .Include(d => d.Logo)
                        .FirstOrDefaultAsync(d => d.Id == request.DieticianId, cancellationToken);

                        if (dietician == null)
                        {
                            return Result<DieticianBusinessCardGetDTO>.Failure("DieticianBusinessCardGetDTO nie znaleziono.");
                        }

                        var dieticianBusinessCardDTO = _mapper.Map<DieticianBusinessCardGetDTO>(dietician);

                        return Result<DieticianBusinessCardGetDTO>.Success(dieticianBusinessCardDTO);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<DieticianBusinessCardGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}