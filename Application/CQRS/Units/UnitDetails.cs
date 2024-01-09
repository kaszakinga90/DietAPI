using Application.Core;
using Application.DTOs.UnitDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Units
{
    public class UnitDetails
    {
        public class Query : IRequest<Result<UnitGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<UnitGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<UnitGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var unit = await _context.UnitsDb
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    if (unit == null)
                    {
                        return Result<UnitGetDTO>.Failure("Unit o podanym id nie został odnaleziony");
                    }

                    return Result<UnitGetDTO>.Success(_mapper.Map<UnitGetDTO>(unit));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<UnitGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}