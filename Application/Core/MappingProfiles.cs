using Application.DTOs.AdminDTO;
using Application.DTOs.DayWeekDTO;
using Application.DTOs.DieteticianPatientDTO;
using Application.DTOs.DieticianDTO;
using Application.DTOs.DishDTO;
using Application.DTOs.MealScheduleDTO;
using Application.DTOs.MealTimeToXYAxisDTO;
using Application.DTOs.PatientDTO;
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

            //CreateMap<Admin, AdminDTO>()
            //    .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
            //    .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));
            //CreateMap<AdminDTO, Admin>();

            CreateMap<Admin, AdminGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<AdminDTO, Admin>();

            // Skomplikowane mapowanie z niestandardową logiką dla dietetyka.

            //CreateMap<Dietician, DieticianDTO>()
            //    .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
            //    .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));
            //CreateMap<DieticianDTO, Dietician>();

            CreateMap<Dietician, DieticianGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<Dietician, DieticianDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));

            CreateMap<DieticianDTO, Dietician>();


            CreateMap<MealTimeToXYAxisDTO, MealTimeToXYAxis>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            //CreateMap<MealTimeToXYAxisDTO, MealTimeToXYAxis>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignoruje Id przy mapowaniu
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.MealTime, opt => opt.MapFrom(src => src.MealTime))
            //    .ForMember(dest => dest.DietId, opt => opt.MapFrom(src => src.DietId));


            //CreateMap<Diet, DietGetDTO>()
            //    .ForMember(dest => dest.MealTimesToXYAxisDTO, opt => opt.MapFrom(src => src.MealTimesToXYAxis));



            CreateMap<Diet, DietDTO>();
            
            //CreateMap<Diet, DietDTO>()
            //    .ForMember(dest => dest.MealTimesToXYAxisDTO, opt => opt.MapFrom(src => src.MealTimesToXYAxis));


            //CreateMap<DietDTO, Diet>();
            CreateMap<DietDTO, Diet>()
                .ForMember(dest => dest.MealTimesToXYAxis, opt => opt.MapFrom(src => src.MealTimesToXYAxisDTO));

            CreateMap<Diet, DietGetDTO>()
                .ForMember(dest => dest.DieteticanName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ReverseMap();

            CreateMap<MealSchedule, MealScheduleGetDTO>()
                .ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.Dish.Name));
            CreateMap<MealScheduleGetDTO, MealSchedule>();

            CreateMap<Dish, DishDTO>();
            CreateMap<DishDTO, Dish>();

            CreateMap<MealScheduleEditDTO, MealSchedule>();
            CreateMap<MealSchedule, MealScheduleEditDTO>();

            CreateMap<DieticianPatient, DieteticianPatientDTO>();
            CreateMap<DieteticianPatientDTO, DieticianPatient>();
        }
    }
}
