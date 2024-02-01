using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Dishes.DishToEdit.Gets
{
    public class DishBaseDetails
    {
        public class Query : IRequest<Result<DishDetailsGetEditDTO>>
        {
            public int DishId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DishDetailsGetEditDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DishDetailsGetEditDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dish = await _context.DishesDb
                        .FirstOrDefaultAsync(d => d.Id == request.DishId);

                    if (dish == null)
                    {
                        return Result<DishDetailsGetEditDTO>.Failure("Nie znaleziono dania o takim Id");
                    }

                    var dishDTO = new DishDetailsGetEditDTO
                    {
                        Id = dish.Id,
                        Name = dish.Name,
                        Calories = dish.Calories,
                        ServingQuantity = dish.ServingQuantity,
                        MeasureId = (int)dish.MeasureId,
                        UnitId = (int)dish.UnitId,
                        GlycemicIndex = dish.GlycemicIndex,
                        PreparingTime = dish.PreparingTime
                    };

                    return Result<DishDetailsGetEditDTO>.Success(dishDTO);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishDetailsGetEditDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}