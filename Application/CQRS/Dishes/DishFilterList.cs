using Application.Core;
using Application.DTOs.DishDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes
{
    public class DishFilterList
    {
        public class Query : IRequest<Result<DishFiltersDTO>>
        {
            public int DishId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishFiltersDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DishFiltersDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var filters = new DishFiltersDTO
                    {
                        DatesAdded = await _context.DieticiansDb
                        .Select(m => m.dateAdded)
                        .Distinct()
                        .ToListAsync(cancellationToken),
                    };

                    return Result<DishFiltersDTO>.Success(filters);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishFiltersDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}