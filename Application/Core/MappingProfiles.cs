﻿using Application.DTOs.AdminDTO;
using Application.DTOs.DayWeekDTO;
using Application.DTOs.DieteticianPatientDTO;
using Application.DTOs.DieticianDTO;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.DishDTO;
using Application.DTOs.IngredientDTO;
using Application.DTOs.IngredientDTO.IngredientNutritionixDTO;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.DTOs.NutrientDTO;
using Application.DTOs.PatientDTO;
using Application.DTOs.SpecializationDTO;
using AutoMapper;
using ModelsDB;
using ModelsDB.Functionality;

namespace Application.Core
{
    /// <summary>
    /// Definiuje profile mapowania dla różnych klas modelu i DTO.
    /// </summary>
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="MappingProfiles"/> i konfiguruje mapowania.
        /// </summary>
        public MappingProfiles()
        {
            // Mapowania dla tych samych typów (dla pełnej konfiguracji).
            CreateMap<CategoryOfDiet, CategoryOfDiet>();

            // Mapowania pomiędzy DTO a modelami.
            CreateMap<DayWeekDTO, DayWeek>();
            CreateMap<DayWeek, DayWeekDTO>();

            CreateMap<MessageTo, MessageToDTO>()
                .ForMember(dest => dest.DieticianName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin.FirstName + " " + src.Admin.LastName))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ReverseMap();

            CreateMap<MessageToDTO, MessageTo>()
            .ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdminId.HasValue ? src.AdminId : null))
            .ForMember(dest => dest.DieticianId, opt => opt.MapFrom(src => src.DieticianId.HasValue ? src.DieticianId : null));

            // Skomplikowane mapowanie z niestandardową logiką dla pacjenta.
            CreateMap<Patient, PatientGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));
            
            CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));
            
            CreateMap<PatientDTO, Patient>();

            CreateMap<PatientEditDataDTO, Patient>();
            CreateMap<Patient, PatientEditDataDTO>();

            // Skomplikowane mapowanie z niestandardową logiką dla admina.

            CreateMap<Admin, AdminGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<AdminDTO, Admin>();

            // Skomplikowane mapowanie z niestandardową logiką dla dietetyka.

            CreateMap<Dietician, DieticianGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<Dietician, DieticianDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<DieticianDTO, Dietician>();


            CreateMap<MealTimeToXYAxisDTO, MealTimeToXYAxis>();
            CreateMap<MealTimeToXYAxis, MealTimeToXYAxisDTO>();

            CreateMap<Diet, DietDTO>();

            CreateMap<DietDTO, Diet>()
                .ForMember(dest => dest.MealTimesToXYAxis, opt => opt.MapFrom(src => src.MealTimesToXYAxisDTO));

            CreateMap<Diet, DietGetDTO>()
                .ForMember(dest => dest.DieteticanName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ReverseMap();

            CreateMap<Dish, DishDTO>();
            CreateMap<DishDTO, Dish>();

            CreateMap<DieticianPatient, DieteticianPatientDTO>();
            CreateMap<DieteticianPatientDTO, DieticianPatient>();

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
            CreateMap<DieticianSpecialization, DieteticianSpecializationGetDTO>();
            CreateMap<DieteticianSpecializationPostDTO, DieticianSpecialization>();

            CreateMap<IngredientDTO, Ingredient>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.IngredientName))
            .ForMember(dest => dest.MeasureId, opt => opt.MapFrom(src => src.MeasureId.Value))
            .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId.Value));

            CreateMap<Ingredient, IngredientDTO>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MeasureId, opt => opt.MapFrom(src => src.Measure.Id))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.Unit.Id));

            CreateMap<Ingredient, IngredientGetDTO>();
        }
    }
}
