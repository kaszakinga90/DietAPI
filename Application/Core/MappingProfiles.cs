﻿using Application.CQRS.Ingredients;
using Application.DTOs;
using Application.DTOs.AddressDTO;
using Application.DTOs.AdminDTO;
using Application.DTOs.BillDTO;
using Application.DTOs.CategoryOfDietDTO;
using Application.DTOs.CountryStateDTO;
using Application.DTOs.DayWeekDTO;
using Application.DTOs.DietDTO;
using Application.DTOs.DieteticianPatientDTO;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.DTOs.DieticianDTO;
using Application.DTOs.DieticianOfficeDTO;
using Application.DTOs.DieticianSpecializationsDTO;
using Application.DTOs.DietPatientDTO;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.DishDTO;
using Application.DTOs.DishFoodCatalogDTO;
using Application.DTOs.DishIngredientDTO;
using Application.DTOs.FoodCatalogDTO;
using Application.DTOs.FoodCatalogDTO.Admin;
using Application.DTOs.IngredientDTO;
using Application.DTOs.IngredientDTO.IngredientNutritionixDTO;
using Application.DTOs.InvitationDTO;
using Application.DTOs.LogoDTO;
using Application.DTOs.MealDTO;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.DTOs.MeasureDTO;
using Application.DTOs.MessagesDTO;
using Application.DTOs.NutrientDTO;
using Application.DTOs.OfficeDTO;
using Application.DTOs.PatientCardDTO;
using Application.DTOs.PatientDTO;
using Application.DTOs.PrintoutsDTO;
using Application.DTOs.RecipeDTO;
using Application.DTOs.RecipeStepDTO;
using Application.DTOs.ReportsClassesDTO;
using Application.DTOs.ReportsClassesDTO.Reports;
using Application.DTOs.ReportTemplateDTO;
using Application.DTOs.RoleDTO;
using Application.DTOs.SexDTO;
using Application.DTOs.SpecializationDTO;
using Application.DTOs.Surveys;
using Application.DTOs.TestsResultsDTO;
using Application.DTOs.UnitDTO;
using AutoMapper;
using DietDB;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        private readonly DietContext _context;

        public MappingProfiles(DietContext context)
        {
            _context = context;
        }

        public MappingProfiles()
        {
            CreateMap<DietSalesBill, DietSalesBillPostDTO>()
            .ReverseMap();

            CreateMap<Sales, SalesPostDTO>()
                .ReverseMap();

            CreateMap<DietSalesBill, DietSalesBillGetDTO>()
            .ReverseMap();

            CreateMap<Sales, SalesGetDTO>()
                .ReverseMap();

            CreateMap<DietSalesBill, DietSalesBillPutDTO>()
            .ReverseMap();

            CreateMap<Sales, SalesPutDTO>()
                .ReverseMap();

            CreateMap<DietPatient, DietPatientPostDTO>()
                .ReverseMap();

            CreateMap<DietPatientPostDTO, DietPatient>()
                .ForMember(dest => dest.DieticianId, opt => opt.MapFrom(src => src.DieticianId))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.DietId, opt => opt.MapFrom(src => src.DietId));
            
            CreateMap<Diet, DietDeleteDTO>()
                .ReverseMap();

            CreateMap<Printout, PrintoutDeleteDTO>()
                   .ReverseMap();

            CreateMap<IngredientEditDTO, Ingredient>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.IngredientName))
                .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.Nutrients))
                .ForMember(dest => dest.MeasureId, opt => opt.MapFrom(src => src.MeasureId))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId))
                .ReverseMap();

            CreateMap<IngredientNutrientDTO, IngredientNutrient>()
                .ReverseMap();

            CreateMap<Ingredient, IngredientDeleteDTO>()
                .ReverseMap();

            CreateMap<CategoryOfDiet, CategoryOfDiet>();
            CreateMap<CategoryOfDiet, CategoryOfDietDeleteDTO>()
                .ReverseMap();

            CreateMap<DayWeekGetDTO, DayWeek>()
                .ReverseMap();

            CreateMap<DayWeek, DayWeekDeleteDTO>()
                .ReverseMap();

            CreateMap<TestResult, TestResultDeleteDTO>()
                .ReverseMap();

            CreateMap<Survey, SurveyDeleteDTO>()
                .ReverseMap();

            CreateMap<Diploma, DiplomaDeleteDTO>()
                .ReverseMap();

            CreateMap<FoodCatalog, FoodCatalogDeleteDTO>()
                .ReverseMap();

            CreateMap<Specialization, SpecializationPostDTO>()
                .ReverseMap();
            
            CreateMap<FoodCatalog, FoodCatalogDieticianEditDTO>()
                .ReverseMap();

            CreateMap<FoodCatalog, FoodCatalogForAdminPostDTO>()
                .ReverseMap();

            CreateMap<Specialization, SpecializationDeleteDTO>()
                .ReverseMap();

            CreateMap<MessageTo, MessageToDTO>()
                .ForMember(dest => dest.DieticianName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin.FirstName + " " + src.Admin.LastName))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ReverseMap();

            CreateMap<MessageToDTO, MessageTo>()
            .ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdminId.HasValue ? src.AdminId : null))
            .ForMember(dest => dest.DieticianId, opt => opt.MapFrom(src => src.DieticianId.HasValue ? src.DieticianId : null));

            CreateMap<Patient, PatientGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<Patient, PatientEditDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<PatientEditDTO, Patient>();

            CreateMap<PatientEditDataDTO, Patient>();
            CreateMap<Patient, PatientEditDataDTO>();

            CreateMap<Admin, AdminGetDTO>()
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.AddressDTO, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<Admin, AdminEditDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<AdminEditDTO, Admin>();

            CreateMap<Admin, AdminPostDTO>()
                .ReverseMap();
            
            CreateMap<Admin, AdminDeleteDTO>()
                .ForMember(dest => dest.AddressDeleteDTO, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<Dietician, DieticianDeleteDTO>()
                .ForMember(dest => dest.AddressDeleteDTO, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.LogoDeleteDTO, opt => opt.MapFrom(src => src.Logo))
                .AfterMap((src, dest) =>
                {
                    dest.DieticianSpecializationDeleteDTO = src.DieticianSpecializations
                        .Select(ds => new DieticianSpecializationDeleteDTO
                        {
                            DieticianId = ds.DieticianId,
                            SpecializationId = ds.SpecializationId,
                            isActive = ds.isActive
                        }).ToList();

                    dest.DieticianOfficesDeleteDTO = src.DieticianOffices
                        .Select(ds => new DieticianOfficeDeleteDTO
                        {
                            DieticianId = ds.DieticianId,
                            OfficeDeleteDTO = new OfficeDeleteDTO
                            {
                                Id = ds.Office.Id,
                                AddressDeleteDTO = new AddressDeleteDTO
                                {
                                    Id = ds.Office.Address.Id,
                                    isActive = ds.Office.Address.isActive
                                },
                                isActive = ds.Office.isActive
                            },
                            isActive = ds.isActive
                        }).ToList();
                })
                .ReverseMap();

            CreateMap<Patient, PatientDeleteDTO>()
                .ForMember(dest => dest.AddressDeleteDTO, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<Address, AddressDeleteDTO>()
                .ReverseMap();

            CreateMap<Logo, LogoDeleteDTO>()
                .ReverseMap();

            CreateMap<DieticianSpecialization, DieticianSpecializationDeleteDTO>()
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
                .ReverseMap();

            CreateMap<DieticianOffice, DieticianOfficeDeleteDTO>()
                .ForMember(dest => dest.DieticianId, opt => opt.MapFrom(src => src.DieticianId))
                .ForMember(dest => dest.OfficeDeleteDTO, opt => opt.MapFrom(src => src.Office))
                .ReverseMap();

            CreateMap<Office, OfficeDeleteDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AddressDeleteDTO, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
                .ReverseMap();

            CreateMap<Role, RolePostDTO>()
                .ReverseMap();
            
            CreateMap<MessageTo, MessageIsReadPostDTO>()
                .ReverseMap();

            CreateMap<Role, RoleGetDTO>()
                .ReverseMap();

            CreateMap<Dietician, DieticianGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<Dietician, DieticianEditDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<DieticianEditDTO, Dietician>();

            CreateMap<MealTimeToXYAxisPostDTO, MealTimeToXYAxis>();
            CreateMap<MealTimeToXYAxis, MealTimeToXYAxisPostDTO>();

            CreateMap<Diet, DietPostDTO>();

            CreateMap<DietPostDTO, Diet>()
                .ForMember(dest => dest.MealTimesToXYAxis, opt => opt.MapFrom(src => src.MealTimesToXYAxisDTO));

            CreateMap<Diet, DietGetDTO>()
                .ForMember(dest => dest.DieteticanName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ReverseMap();

            CreateMap<Dish, DishGetDTO>()
                .ReverseMap();

            CreateMap<Dish, DishDeleteDTO>();

            CreateMap<Meal, MealGetDTO>()
                .ReverseMap();
            
            CreateMap<Survey, SurveyPostDTO>()
                .ReverseMap();
            
            CreateMap<SingleTestResults, TestResultPostDTO>()
                .ReverseMap();

            CreateMap<DieticianPatient, DieteticianPatientGetDTO>()
                .ReverseMap();

            CreateMap<IngredientDTO, Ingredient>()
                .ForMember(dest => dest.Measure, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore())
                .ForMember(dest => dest.Nutrients, opt => opt.Ignore())
                .ForMember(dest => dest.DishIngredients, opt => opt.Ignore());

            CreateMap<IngredientNutritionixDTO, Ingredient>()
                .ForMember(dest => dest.Measure, opt => opt.Ignore())
                .ForMember(dest => dest.Unit, opt => opt.Ignore())
                .ForMember(dest => dest.Nutrients, opt => opt.Ignore())
                .ForMember(dest => dest.DishIngredients, opt => opt.Ignore());

            CreateMap<Ingredient, IngredientNutritionixDTO>()
            .ForMember(dest => dest.MeasureId, opt => opt.MapFrom(src => src.MeasureId))
            .ForMember(dest => dest.MeasureNameFromJSON, opt => opt.MapFrom(src => src.Measure.Symbol))
            .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId))
            .ForMember(dest => dest.GlycemicIndex, opt => opt.MapFrom(src => src.GlycemicIndex))
            .ForMember(dest => dest.NutrientsDTO, opt => opt.MapFrom(src => src.Nutrients));

            CreateMap<NutrientDTO, Nutrient>()
            .ForMember(dest => dest.Unit, opt => opt.Ignore())
            .ForMember(dest => dest.Ingredients, opt => opt.Ignore());

            CreateMap<IngredientNutrientDTO, IngredientNutrient>()
                .ForMember(dest => dest.Ingredient, opt => opt.Ignore())
                .ForMember(dest => dest.Nutrient, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<DiplomaPostDTO, Diploma>();
            CreateMap<Diploma, DiplomaPostDTO>();

            CreateMap<Diploma, DiplomaGetDTO>();
            CreateMap<Specialization, SpecializationGetDTO>();

            CreateMap<DieticianSpecialization, DieteticianSpecializationPostDTO>()
           .ForMember(dest => dest.DieticianId, opt => opt.MapFrom(src => src.DieticianId))
           .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.SpecializationId))
           .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.Specialization.SpecializationName));

            CreateMap<DieteticianSpecializationPostDTO, DieticianSpecialization>();

            CreateMap<IngredientDTO, Ingredient>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.IngredientName))
            .ForMember(dest => dest.MeasureId, opt => opt.MapFrom(src => src.MeasureId.Value))
            .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId.Value))
                .ReverseMap();

            CreateMap<Ingredient, IngredientDTO>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MeasureId, opt => opt.MapFrom(src => src.Measure.Id))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.Id));

            CreateMap<Ingredient, IngredientGetDTO>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Unit, UnitGetDTO>()
                .ReverseMap();

            CreateMap<Measure, MeasureGetDTO>()
                .ReverseMap();

            CreateMap<Ingredient, IngredientGetDTO>();

            CreateMap<DishPostDTO, Dish>()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore());

            CreateMap<Dish, DishPostDTO>();

            CreateMap<Dish, DishEditDTO>();

            CreateMap<DishFoodCatalog, DishFoodCatalogPostDTO>()
                .ReverseMap();

            CreateMap<DishIngredientPostDTO, DishIngredient>()
                .ReverseMap();

            CreateMap<DishIngredient, DishIngredientPutDTO>()
                .ReverseMap();

            CreateMap<RecipePostDTO, Recipe>()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ReverseMap();

            CreateMap<RecipeStepPostDTO, RecipeStep>()
                .ReverseMap();

            CreateMap<FoodCatalogGetDTO, FoodCatalog>()
                .ReverseMap();

            CreateMap<FoodCatalogPostDTO, FoodCatalog>()
                .ReverseMap();

            CreateMap<MealTimeToXYAxisEditDTO, MealTimeToXYAxis>()
                .ReverseMap();

            CreateMap<PatientCard, PatientCardGetDTO>()
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
            .ForMember(dest => dest.SexId, opt => opt.MapFrom(src => src.SexId))
            .ForMember(dest => dest.DieticianId, opt => opt.MapFrom(src => src.DieticianId))
            .ForMember(dest => dest.PatientCardSurveys, opt => opt.MapFrom(src => src.PatientCardSurveys))
            .ForMember(dest => dest.TestResults, opt => opt.MapFrom(src => src.TestResults));

            CreateMap<PatientCardPostDTO, PatientCard>()
                .ReverseMap();

            CreateMap<Sex, SexGetDTO>()
                .ReverseMap();

            CreateMap<Logo, LogoPostDTO>()
                .ReverseMap();

            CreateMap<Logo, LogoGetDTO>()
                .ReverseMap();

            CreateMap<Dietician, DieticianEditDataDTO>()
                .ReverseMap();

            CreateMap<Address, AddressPostDTO>()
                .ReverseMap();

            CreateMap<Office, OfficePostDTO>()
                .ReverseMap();

            CreateMap<DieticianOffice, DieticianOfficesGetDTO>();

            CreateMap<ReportTemplate, ReportTemplateGetDTO>()
                .ReverseMap();

            CreateMap<ReportTemplate, ReportTemplateDeleteDTO>()
                .ReverseMap();

            CreateMap<Dietician, DieticianEditDataDTO>()
                .ReverseMap();
            
            CreateMap<PatientCard, PatientCardGetDTO>()
                .ReverseMap();

            CreateMap<Office, OfficeEditDTO>()
                       .ForMember(dest => dest.AddressDTO, opt => opt.MapFrom(src => src.Address))
                       .ReverseMap();

            CreateMap<Address, AddressesDTO>().ReverseMap();

            CreateMap<InvitationPostDTO, Invitation>().ReverseMap();
            CreateMap<Invitation, InvitationPutDTO>().ReverseMap();
            CreateMap<Invitation, InvitationGetDTO>().ReverseMap();

            CreateMap<Dietician, DieticianBusinessCardGetDTO>()
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                      .ForMember(dest => dest.DieticianName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                      .ForMember(dest => dest.DieticianPictureUrl, opt => opt.MapFrom(src => src.PictureUrl))
                      .ForMember(dest => dest.DieticianLogoUrl, opt => opt.MapFrom(src => src.Logo.PictureUrl))
                      .ForMember(dest => dest.DieticianOffices, opt => opt.MapFrom(src => src.DieticianOffices.Select(src => src.Office))) 
                      .ForMember(dest => dest.DieticianDiplomas, opt => opt.MapFrom(src => src.Diplomas))
                      .ForMember(dest => dest.DieticianSpecializations, opt => opt.MapFrom(src => src.DieticianSpecializations.Select(ds => ds.Specialization)));

            CreateMap<Office, OfficeGetDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.OfficeName))
                    .ForMember(dest => dest.AddressDTO, opt => opt.MapFrom(src => src.Address));

            CreateMap<Diploma, DiplomaGetDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.PictureUrl));

            CreateMap<ReportTemplatePreview, ReportTemplatePreviewDTO>()
                .ReverseMap();

            CreateMap<Diet, DietForPatientToDocumentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ForMember(dest => dest.DieticianName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ForMember(dest => dest.DieticianLogoUrl, opt => opt.MapFrom(src => src.Dietician.Logo.PictureUrl))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToShortDateString()))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToShortDateString()))
                .ForMember(dest => dest.numberOfMeals, opt => opt.MapFrom(src => src.numberOfMeals))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => (int)(src.EndDate - src.StartDate).TotalDays))
                .ForMember(dest => dest.MealTimesToXYAxisDTO, opt => opt.MapFrom(src => src.MealTimesToXYAxis.Select(mt => new MealTimeToXYAxisToReportDTO
                {
                    DishName = mt.Dish.Name,
                    MealTime = mt.MealTime.ToString(),
                    MealName = mt.Meal.Name,
                    Dish = new DishToReportDTO
                    {
                        Name = mt.Dish.Name,
                        Calories = mt.Dish.Calories,
                        ServingQuantity = mt.Dish.ServingQuantity,
                        MeasureName = mt.Dish.Measure.Symbol,
                        UnitName = mt.Dish.Unit.Symbol,
                        GlycemicIndex = mt.Dish.GlycemicIndex,
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
                }).ToList()));
        }
    }
}
