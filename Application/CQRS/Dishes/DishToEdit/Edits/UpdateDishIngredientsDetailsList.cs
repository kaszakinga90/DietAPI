using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using Application.Validators.DishEditDetails;
using DietDB;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;
using System.Diagnostics;

namespace Application.CQRS.Dishes.DishToEdit.Edits
{
    public class UpdateDishIngredientsDetailsList
    {
        public class Command : IRequest<Result<List<DishIngredientsDetailsGetEditDTO>>>
        {
            public int DishId { get; set; }
            public List<DishIngredientsDetailsGetEditDTO> DishIngredientsDetailsGetEditDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<List<DishIngredientsDetailsGetEditDTO>>>
        {
            private readonly DietContext _context;
            private readonly DishIngredientDetailsUpdateValidator _validator;

            public Handler(DietContext context, DishIngredientDetailsUpdateValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<Result<List<DishIngredientsDetailsGetEditDTO>>> Handle(Command request, CancellationToken cancellationToken)
            {
                foreach (var item in request.DishIngredientsDetailsGetEditDto)
                {
                    var validationResult = await _validator
                    .ValidateAsync(item);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<List<DishIngredientsDetailsGetEditDTO>>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }
                }

                try
                {
                    var existingIngredients = await _context.DishIngredientsDb
                        .Where(i => i.DishId == request.DishId)
                        .ToListAsync();

                    foreach (var existingIngredient in existingIngredients)
                    {
                        var dto = request.DishIngredientsDetailsGetEditDto.FirstOrDefault(d => d.IngredientId == existingIngredient.IngredientId);

                        if (dto != null)
                        {
                            existingIngredient.Quantity = dto.Quantity;
                            existingIngredient.UnitId = dto.UnitId;
                        }
                    }

                    var ingredientsToDelete = existingIngredients.Where(i => !request.DishIngredientsDetailsGetEditDto.Any(dto => dto.IngredientId == i.IngredientId)).ToList();
                    _context.DishIngredientsDb.RemoveRange(ingredientsToDelete);

                    var ingredientsToAdd = request.DishIngredientsDetailsGetEditDto.Where(dto => !existingIngredients.Any(i => i.IngredientId == dto.IngredientId));
                    
                    foreach (var dto in ingredientsToAdd)
                    {
                        _context.DishIngredientsDb.Add(new DishIngredient
                        {
                            DishId = dto.DishId,
                            IngredientId = dto.IngredientId,
                            Quantity = dto.Quantity,
                            UnitId = dto.UnitId
                        });
                    }

                    await _context.SaveChangesAsync();

                    return Result<List<DishIngredientsDetailsGetEditDTO>>.Success(request.DishIngredientsDetailsGetEditDto.ToList());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DishIngredientsDetailsGetEditDTO>>.Failure("Wystąpił błąd podczas aktualizacji danych.");
                }
            }
        }
    }
}
