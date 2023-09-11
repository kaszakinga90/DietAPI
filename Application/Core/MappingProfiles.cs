using AutoMapper;
using ModelsDB;
using ModelsDB.Functionality;
using ModelsDB.Layout;
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
            CreateMap<Carousel, Carousel>();
            CreateMap<Article, Article>();
            CreateMap<Footer, Footer>();
            CreateMap<LayoutCategory, LayoutCategory>();
            CreateMap<LayoutPhoto, LayoutPhoto>();
            CreateMap<Link, Link>();
            CreateMap<MainNavbar, MainNavbar>();
            CreateMap<News, News>();
            CreateMap<SocialMedia, SocialMedia>();
            CreateMap<SubTab, SubTab>();
            CreateMap<Tab, Tab>();
            CreateMap<Tag, Tag>();
        }
    }
}
