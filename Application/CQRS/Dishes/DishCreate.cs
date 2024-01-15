﻿using Application.Core;
using Application.DTOs.DishDTO;
using Application.Services;
using Application.Validators.Dish;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Http;
using ModelsDB;
using ModelsDB.Functionality;

// TODO : obsługa - dokończ.
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
            private readonly DishCreateValidator _validator;

            public Handler(DietContext context, IMapper mapper, ImageService imageService, DishCreateValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _imageService = imageService;
                _validator = validator;
            }

            public async Task<Result<DishPostDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator
                    .ValidateAsync(request.DishPostDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage.ToString()).ToList();
                    return Result<DishPostDTO>.Failure("Wystąpiły błędy walidacji: \n" + string.Join("\n", errors));
                }

                var requestDish = request.DishPostDTO;

                var dish = new Dish
                {
                    Id = requestDish.Id,
                    Name = requestDish.Name,
                    Calories = requestDish.Calories,
                    ServingQuantity = requestDish.ServingQuantity,
                    MeasureId = requestDish.MeasureId,
                    Weight = requestDish.Weight,
                    UnitId = requestDish.UnitId,
                    GlycemicIndex = requestDish.GlycemicIndex,
                    PreparingTime = requestDish.PreparingTime,
                    DieticianId = requestDish.DieteticianId,
                    DishFoodCatalogs = requestDish.DishFoodCatalogs?.Select(dto => _mapper.Map<DishFoodCatalog>(dto)).ToList(),
                    DishIngredients = requestDish.DishIngredients?.Select(dto => _mapper.Map<DishIngredient>(dto)).ToList(),
                    //MealTimes = requestDish.MealTimes?.Select(dto => _mapper.Map<MealTimeToXYAxisEditDTO>(dto)).ToList(),
                };

                //if (requestDish.File != null)
                //{
                //    var imageResult = await _imageService.AddImageAsync(requestDish.File);

                //    dish.DishPhotoUrl = imageResult.SecureUrl.ToString();
                //    dish.PublicId = imageResult.PublicId;
                //}

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

                return Result<DishPostDTO>.Success(_mapper.Map<DishPostDTO>(dish));
            }

        }
    }
}

// TODO : optymalizacja
// powyższa metoda po optymalizacji - propozycja:
//public async Task<Result<DishPostDTO>> Handle(Command request, CancellationToken cancellationToken)
//{
//    using (var transaction = await _context.Database.BeginTransactionAsync())
//    {
//        try
//        {
//            var dish = _mapper.Map<Dish>(request.DishPostDTO);
//            _context.DishesDb.Add(dish);
//            await _context.SaveChangesAsync();

//            var recipe = new Recipe() { DishId = dish.Id };
//            _context.RecipesDb.Add(recipe);
//            await _context.SaveChangesAsync();

//            dish.RecipeId = recipe.Id;
//            await _context.SaveChangesAsync();

//            var existingRecipe = await _context.RecipesDb.FindAsync(recipe.Id);
//            existingRecipe.Steps = _mapper.Map<List<RecipeStep>>(request.DishPostDTO.RecipeSteps);
//            await _context.SaveChangesAsync();

//            await transaction.Commit();
//            return Result<DishPostDTO>.Success(_mapper.Map<DishPostDTO>(dish));
//        }
//        catch (Exception ex)
//        {
//            await transaction.RollbackAsync();
//            Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
//            return Result<DishPostDTO>.Failure("Wystąpił błąd podczas przetwarzania danych.");
//        }
//    }
//}