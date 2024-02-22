using Application.Core;
using Application.DTOs.SexDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Sexes
{
    public class SexesList
    {
        public class Query : IRequest<Result<List<SexGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<SexGetDTO>>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<SexGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var sexesListFromDb = await _context.SexesDb
                    .FromSqlRaw("SELECT * FROM GetAllSexesFromSqlView")
                    .ToListAsync();

                    if (sexesListFromDb == null)
                    {
                        return Result<List<SexGetDTO>>.Failure("Brak wyników");
                    }

                    var sexesList = _mapper.Map<List<SexGetDTO>>(sexesListFromDb);

                    return Result<List<SexGetDTO>>.Success(sexesList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<SexGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}