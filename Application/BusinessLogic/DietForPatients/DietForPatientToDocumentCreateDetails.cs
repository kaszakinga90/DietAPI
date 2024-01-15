﻿using Application.Core;
using Application.DTOs.ReportsClassesDTO;
using Application.DTOs.ReportsClassesDTO.Reports;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.DietForPatients
{
    public class DietForPatientToDocumentCreateDetails
    {
        public class Command : IRequest<Result<DietForPatientToDocumentDTO>>
        {
            public int DietId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<DietForPatientToDocumentDTO>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<DietForPatientToDocumentDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dietForPatient = await _context.DietsDb
                    .Where(m => m.Id == request.DietId)
                    .Include(diet => diet.Patient)
                    .Include(mt => mt.MealTimesToXYAxis)
                        .ThenInclude(mt => mt.Meal)
                    .Include(diet => diet.MealTimesToXYAxis)
                        .ThenInclude(mt => mt.Dish)
                            .ThenInclude(dish => dish.Recipe)
                                .ThenInclude(recipe => recipe.Steps)
                    .Include(mt => mt.MealTimesToXYAxis)
                        .ThenInclude(mt => mt.Dish)
                            .ThenInclude(dish => dish.DishIngredients)
                                .ThenInclude(dish => dish.Ingredient)
                    .Select(d => new DietForPatientToDocumentDTO
                    {
                        Name = d.Name,
                        PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                        DieticianName = d.Dietician.FirstName + " " + d.Dietician.LastName,
                        DieticianLogoUrl = d.Dietician.Logo.PictureUrl,
                        StartDate = d.StartDate.ToShortDateString(),
                        EndDate = d.EndDate.ToShortDateString(),
                        numberOfMeals = d.numberOfMeals,
                        Period = (int)(d.EndDate - d.StartDate).TotalDays + 1,
                        MealTimesToXYAxisDTO = d.MealTimesToXYAxis
                        .Select(mt => new MealTimeToXYAxisToReportDTO
                        {
                            MealTime = mt.MealTime.ToString(),
                            DishName = mt.Dish.Name,
                            MealName = mt.Meal.Name,
                            Dish = new DishToReportDTO
                            {
                                Name = mt.Dish.Name,
                                Calories = mt.Dish.Calories,
                                ServingQuantity = mt.Dish.ServingQuantity,
                                MeasureName = mt.Dish.Measure.Symbol,
                                Weight = mt.Dish.Weight, 
                                UnitName = mt.Dish.Unit.Symbol,
                                GlycemicIndex = mt.Dish.GlycemicIndex,
                                DishPhotoUrl = mt.Dish.DishPhotoUrl,
                                PreparingTime = mt.Dish.PreparingTime,
                                Recipe = new RecipeToReportDTO
                                {
                                    Steps = mt.Dish.Recipe.Steps.Select(step => new RecipeStepToReportDTO
                                    {
                                        StepNumber = step.StepNumber,
                                        Description = step.Description,
                                    }).ToList(),
                                },
                                Ingredients = mt.Dish.DishIngredients.Select(di => new DishIngredientToReportDTO
                                {
                                    Quantity = di.Quantity,
                                    UnitName = di.Unit.Symbol,
                                    IngredientName = di.Ingredient.Name,
                                }).ToList(),
                            },
                        }).ToList(),
                    })
                    .FirstOrDefaultAsync();

                if (dietForPatient == null)
                {
                    return Result<DietForPatientToDocumentDTO>.Failure("cannot create diet for patient to document.");
                }

                return Result<DietForPatientToDocumentDTO>.Success(dietForPatient);
            }


            //public async Task<Result<DietForPatientToDocumentDTO>> Handle(Command request, CancellationToken cancellationToken)
            //{
            //    var dietForPatient = await _context.DietsDb
            //        .Where(m => m.Id == request.DietId)
            //        .Include(diet => diet.PatientEditDTO)
            //        .Include(mt => mt.MealTimesToXYAxis)
            //            .ThenInclude(mt => mt.Meal)
            //        .Include(diet => diet.MealTimesToXYAxis)
            //            .ThenInclude(mt => mt.Dish)
            //                .ThenInclude(dish => dish.Recipe)
            //                    .ThenInclude(recipe => recipe.Steps)
            //        .Include(diet => diet.MealTimesToXYAxis)
            //            .ThenInclude(mt => mt.Dish)
            //                .ThenInclude(dish => dish.DishIngredients) 
            //                    .ThenInclude(di => di.Ingredient)
            //        .Select(d => new DietForPatientToDocumentDTO
            //        {
            //            Name = d.Name,
            //            PatientName = d.PatientEditDTO.FirstName + " " + d.PatientEditDTO.LastName,
            //            DieticianName = d.DieticianEditDTO.FirstName + " " + d.DieticianEditDTO.LastName,
            //            DieticianLogoUrl = d.DieticianEditDTO.Logo.PictureUrl,
            //            StartDate = d.StartDate.ToShortDateString(),
            //            EndDate = d.EndDate.ToShortDateString(),
            //            numberOfMeals = d.numberOfMeals,
            //            Period = (int)(d.EndDate - d.StartDate).TotalDays + 1,
            //            MealTimesToXYAxisDTO = d.MealTimesToXYAxis.Select(mt => new MealTimeToXYAxisToReportDTO
            //            {
            //                MealTime = mt.MealTime.ToString(),
            //                DishName = mt.Dish.Name,
            //            }).ToList(),
            //            DishesDTO = d.MealTimesToXYAxis.Select(mt => mt.Dish).Select(dish => new DishGetDTO
            //            {
            //                Name = dish.Name,
            //                Calories = dish.Calories,
            //                ServingQuantity = dish.ServingQuantity,
            //                MeasureId = (int)dish.MeasureId,
            //                Weight = dish.Weight,
            //                UnitId = (int)dish.UnitId,
            //                GlycemicIndex = dish.GlycemicIndex,
            //                DishPhotoUrl = dish.DishPhotoUrl,
            //                PreparingTime = dish.PreparingTime,
            //                RecipeId = dish.Recipe.Id,
            //                RecipeStepsDTO = dish.Recipe.Steps.Select(step => new RecipeStepGetDTO
            //                {
            //                    Id = step.Id,
            //                    StepNumber = step.StepNumber,
            //                    Description = step.Description,
            //                    RecipeId = step.RecipeId
            //                }).ToList(),
            //                DishIngredients = dish.DishIngredients.Select(di => new DishIngredientGetDTO
            //                {
            //                    DishId = di.DishId,
            //                    DishName = di.Dish.Name,
            //                    IngredientId = di.IngredientId,
            //                    IngredientName = di.Ingredient.Name,
            //                    Quantity = di.Quantity,
            //                    UnitId = di.UnitId
            //                }).ToList(),
            //            }).ToList(),
            //        })
            //        .FirstOrDefaultAsync();

            //    if (dietForPatient == null)
            //    {
            //        return Result<DietForPatientToDocumentDTO>.Failure("cannot create diet for patient to document.");
            //    }

            //    return Result<DietForPatientToDocumentDTO>.Success(dietForPatient);
            //}
        }
    }
}
