using Application.DTOs.DayWeekDTO;
using Application.DTOs.MessagesDTO;
using AutoMapper;
using ModelsDB;
using ModelsDB.Functionality;
using ModelsDB.Layout;
using ModelsDB.ManualPanel;

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
            CreateMap<MessageToDieteticianDTO, MessageToDietician>();
            CreateMap<Message, MessageToPatientDTO>();

            CreateMap<MessageToPatient, MessageToPatientDTO>()
                .ForMember(dest => dest.DieticianName, opt => opt.MapFrom(src => src.Dietician.FirstName + " " + src.Dietician.LastName))
                .ReverseMap();

            CreateMap<MessageToPatientDTO, Message>().ReverseMap();

            // Skomplikowane mapowanie z niestandardową logiką dla pacjenta.
            CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.Date : (DateTime?)null))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.MessageToPatients));
            CreateMap<PatientDTO, Patient>();
        }
    }
}
