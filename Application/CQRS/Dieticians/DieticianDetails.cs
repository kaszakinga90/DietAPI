using Application.Core;
using Application.DTOs.DieticianDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Application.CQRS.Dieticians
{
    public class DieticianDetails
    {
        public class Query : IRequest<Result<DieticianGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DieticianGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DieticianGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dietician = await _context.DieticiansDb
                          .Include(p => p.Address)
                          .Include(p => p.MessageTo)
                          .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    if (dietician == null)
                    {
                        return Result<DieticianGetDTO>.Failure("Dietician nie został znaleziony.");
                    }

                    return Result<DieticianGetDTO>.Success(_mapper.Map<DieticianGetDTO>(dietician));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DieticianGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}