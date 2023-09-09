using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;
using ModelsDB.Layout;
using ModelsDB.ManualPanel;

namespace DietDB
{
    public class DietContext : DbContext
    {
        public DietContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Example> Examples { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CategoryOfDiet> CategoryOfDiet { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DayWeek> DayWeek { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Dietician> Dieticians { get; set; }
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<FoodCatalog> FoodCatalogs { get; set; }
        public DbSet<Ingredient> Ingridients { get; set; }
        public DbSet<MealTime> MealTime { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientCard> PatientCards { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<SingleDiet> SingleDiet { get; set; }
        public DbSet<SingleTestEqual> SingleTestEqual { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<TestEqual> TestEquals { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<LayoutCategory> LayoutCategories { get; set; }
        public DbSet<LayoutPhoto> LayoutPhotos { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<MainNavbar> Navbars { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<SubTab> SubTabs { get; set; }
        public DbSet<Tab> Tabs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<FileCategory> FileCategories { get; set; }
        public DbSet<Manual> Manuals { get; set; }
        public DbSet<Tooltip> Tooltips { get; set; }
    }
}
