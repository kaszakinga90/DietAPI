using Application.Core;
using Application.DTOs.DishDetailsToEditDTO;
using Application.Validators.DishEditDetails;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsDB.Functionality;

namespace Application.CQRS.Dishes.DishToEdit.Edits
{
    public class UpdateDishRecipeDetails
    {
        public class Command : IRequest<Result<DishRecipeDetailsGetEditDTO>>
        {
            public int DishId { get; set; }
            public DishRecipeDetailsGetEditDTO DishRecipeDetailsGetEditDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<DishRecipeDetailsGetEditDTO>>
        {
            private readonly DietContext _context;
            private readonly DishRecipeDetailsUpdateValidator _validator;

            public Handler(DietContext context, DishRecipeDetailsUpdateValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<Result<DishRecipeDetailsGetEditDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dtosRecipeStepsList = request.DishRecipeDetailsGetEditDto.RecipeStepsDTO;

                foreach (var item in dtosRecipeStepsList)
                {
                    var validationResult = await _validator.ValidateAsync(item);
                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                        return Result<DishRecipeDetailsGetEditDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                    }
                }

                var dish = await _context.DishesDb.FindAsync(request.DishId);
                if (dish == null)
                {
                    return Result<DishRecipeDetailsGetEditDTO>.Failure("Dish o podanym ID nie zostało znalezione.");
                }

                var existingRecipeSteps = await _context.RecipeStepsDb
                    .Where(rs => rs.RecipeId == dish.RecipeId)
                    .ToListAsync();

                foreach (var dto in dtosRecipeStepsList)
                {
                    var existingStep = existingRecipeSteps.FirstOrDefault(rs => rs.Id == dto.Id);
                    if (existingStep != null)
                    {
                        existingStep.StepNumber = dto.StepNumber;
                        existingStep.Description = dto.Description;
                    }
                    else
                    {
                        var newStep = new RecipeStep
                        {
                            StepNumber = dto.StepNumber,
                            Description = dto.Description,
                            RecipeId = request.DishRecipeDetailsGetEditDto.RecipeId
                        };
                        _context.RecipeStepsDb.Add(newStep);
                    }
                }

                var stepIdsToRemove = existingRecipeSteps.Where(rs => !dtosRecipeStepsList.Select(dto => dto.Id).Contains(rs.Id)).Select(rs => rs.Id).ToList();
                var stepsToRemove = await _context.RecipeStepsDb.Where(rs => stepIdsToRemove.Contains(rs.Id)).ToListAsync();
                _context.RecipeStepsDb.RemoveRange(stepsToRemove);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Result<DishRecipeDetailsGetEditDTO>.Failure($"Wystąpił błąd podczas zapisywania zmian w bazie danych: {ex.Message}");
                }

                var dishDto = new DishRecipeDetailsGetEditDTO()
                {
                    Id = dish.Id,
                    RecipeId = (int)dish.RecipeId,
                    RecipeStepsDTO = await _context.RecipeStepsDb
                        .Where(rs => rs.RecipeId == dish.RecipeId)
                        .Select(rs => new RecipeStepGetEditDTO
                        {
                            Id = rs.Id,
                            StepNumber = rs.StepNumber,
                            Description = rs.Description
                        })
                        .ToListAsync()
                };

                return Result<DishRecipeDetailsGetEditDTO>.Success(dishDto);
            }
        }
    }
}
