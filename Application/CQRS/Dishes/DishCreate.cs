using Application.Core;
using Application.DTOs.DishDTO;
using Application.DTOs.RecipeDTO;
using Application.DTOs.RecipeStepDTO;
using Application.Services;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;
using ModelsDB.ManualPanel;

namespace Application.CQRS.Dishes
{
    public class DishCreate
    {
        public class Command : IRequest<Result<DishPostDTO>>
        {
            public DishPostDTO DishPostDTO { get; set; }
            public IFormFile File { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<DishPostDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;
            private readonly ImageService _imageService;
            public Handler(DietContext context, IMapper mapper, ImageService imageService)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
            }

            public async Task<Result<DishPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                //    //using (var transaction = _context.Database.BeginTransaction())
                //    //{
                //    //    try
                //    //    {
                var requestDish = request.DishPostDTO;

                var dish = new Dish
                {
                    Id = requestDish.Id,
                    Name = requestDish.DishName,
                    Calories = requestDish.Calories,
                    ServingQuantity = requestDish.ServingQuantity,
                    MeasureId = requestDish.MeasureId,
                    Weight = requestDish.Weight,
                    UnitId = requestDish.UnitId,
                    GlycemicIndex = requestDish.GlycemicIndex,
                    PreparingTime = requestDish.PreparingTime,

                    DishFoodCatalogs = requestDish.DishFoodCatalogs?.Select(dto => _mapper.Map<DishFoodCatalog>(dto)).ToList(),
                    DishIngredients = requestDish.DishIngredients?.Select(dto => _mapper.Map<DishIngredient>(dto)).ToList(),
                    MealTimes = requestDish.MealTimes?.Select(dto => _mapper.Map<MealTimeToXYAxis>(dto)).ToList(),
                };

                _context.DishesDb.Add(dish);
                await _context.SaveChangesAsync();

                var recipe = new Recipe()
                {
                    DishId = dish.Id
                };

                _context.RecipesDb.Add(recipe);
                await _context.SaveChangesAsync();

                var createdRecipeId = recipe.Id;
                dish.RecipeId = createdRecipeId;
                await _context.SaveChangesAsync();

                var existingRecipe = await _context.RecipesDb.FindAsync(createdRecipeId);

                existingRecipe.Steps = _mapper.Map<List<RecipeStep>>(requestDish.RecipeSteps);
                await _context.SaveChangesAsync();

                //    //    transaction.Commit();
                //    //}
                //    //catch (Exception ex)
                //    //{
                //    //    transaction.Rollback();
                //    //    return Result<DishPostDTO>.Failure("Wystąpił błąd podczas zapisywania danych do bazy.");
                //    //}
                //    return Result<DishPostDTO>.Success(_mapper.Map<DishPostDTO>(dish));
                //    //}
                return Result<DishPostDTO>.Success(_mapper.Map<DishPostDTO>(dish));
            }

        }
    }
}
