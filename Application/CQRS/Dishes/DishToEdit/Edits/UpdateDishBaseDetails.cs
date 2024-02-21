using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using Application.Validators.DishEditDetails;
using DietDB;
using MediatR;
using System.Diagnostics;

namespace Application.CQRS.Dishes.DishToEdit.Edits
{
    public class UpdateDishBaseDetails
    {
        public class Command : IRequest<Result<DishDetailsGetEditDTO>>
        {
            public int DishId { get; set; }
            public DishDetailsGetEditDTO DishDetailsGetEditDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<DishDetailsGetEditDTO>>
        {
            private readonly DietContext _context;
            private readonly DishBaseDetailsUpdateValidator _validator;

            public Handler(DietContext context, DishBaseDetailsUpdateValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<Result<DishDetailsGetEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request.DishDetailsGetEditDto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<DishDetailsGetEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var dish = await _context.DishesDb.FindAsync(request.DishId);
                if (dish == null)
                {
                    return Result<DishDetailsGetEditDTO>.Failure("Dish o podanym ID nie zostało znalezione.");
                }

                var req = request.DishDetailsGetEditDto;
                if (req.Name != null)
                    dish.Name = req.Name;
                if (req.Calories != null)
                    dish.Calories = req.Calories;
                if (req.ServingQuantity != null)
                    dish.ServingQuantity = req.ServingQuantity;
                if (req.MeasureId != null)
                    dish.MeasureId = req.MeasureId;
                if (req.UnitId != null)
                    dish.UnitId = req.UnitId;
                if (req.GlycemicIndex != null)
                    dish.GlycemicIndex = req.GlycemicIndex;
                if (req.PreparingTime != null)
                    dish.PreparingTime = req.PreparingTime;

                try
                {
                    var result = await _context.SaveChangesAsync() > 0;
                    if (!result)
                    {
                        return Result<DishDetailsGetEditDTO>.Failure("Edycja dish base details nie powiodła się.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DishDetailsGetEditDTO>.Failure("Wystąpił błąd podczas edycji dish base details.");
                }

                var dishDto = new DishDetailsGetEditDTO()
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Calories = dish.Calories,
                    ServingQuantity = dish.ServingQuantity,
                    MeasureId = dish.MeasureId,
                    UnitId = dish.UnitId,
                    GlycemicIndex = dish.GlycemicIndex,
                    PreparingTime = dish.PreparingTime
                };

                return Result<DishDetailsGetEditDTO>.Success(dishDto);
            }
        }
    }
}