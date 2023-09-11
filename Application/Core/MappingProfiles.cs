using AutoMapper;
using ModelsDB;
using ModelsDB.Functionality;
using ModelsDB.ManualPanel;

namespace Application.Core
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Example, Example>();
            CreateMap<DayWeek, DayWeek>();
            CreateMap<CategoryOfDiet, CategoryOfDiet>();
            CreateMap<SingleDiet, SingleDiet>();
            CreateMap<Tooltip, Tooltip>();
        }
    }
}
