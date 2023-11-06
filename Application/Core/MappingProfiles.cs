using Application.DTOs.AdminDTO;
using Application.DTOs.DayWeekDTO;
using Application.DTOs.DieticianDTO;
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
            CreateMap<Example, Example>();
            CreateMap<CategoryOfDiet, CategoryOfDiet>();
            CreateMap<SingleDiet, SingleDiet>();

            // Mapowania pomiędzy DTO a modelami.
            CreateMap<DayWeekDTO, DayWeek>();
            CreateMap<DayWeek, DayWeekDTO>();
            CreateMap<MessageToDTO, MessageTo>();
            CreateMap<MessageTo, MessageToDTO>();

            CreateMap<MessageTo, MessageToDTO>()
                .ForMember(dest => dest.DieticianName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin.FirstName + " " + src.Admin.LastName))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ReverseMap();

            CreateMap<MessageToDTO, MessageTo>().ReverseMap();

            // Skomplikowane mapowanie z niestandardową logiką dla pacjenta.
            CreateMap<Patient, PatientGetDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageToPatients));
            CreateMap<PatientDTO, Patient>();CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));
            CreateMap<PatientDTO, Patient>();

            // Skomplikowane mapowanie z niestandardową logiką dla admina.
            CreateMap<Admin, AdminDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));
            CreateMap<AdminDTO, Admin>();

            // Skomplikowane mapowanie z niestandardową logiką dla admina.
            CreateMap<Dietician, DieticianDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageTo));
            CreateMap<DieticianDTO, Dietician>();
        }
    }
}
