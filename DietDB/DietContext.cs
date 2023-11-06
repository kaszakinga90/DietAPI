using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;
using ModelsDB.Layout;
using ModelsDB.ManualPanel;
using System.Diagnostics;

namespace DietDB
{
    /// <summary>
    /// Reprezentuje kontekst bazy danych dla diet.
    /// </summary>
    public class DietContext : DbContext
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="DietContext"/> z określonymi opcjami.
        /// </summary>
        /// <param name="options">Opcje dla kontekstu bazy danych.</param>
        public DietContext(DbContextOptions options) : base(options)
        {
        }
        #region DbSet
        /// <summary>
        /// Pobiera lub ustawia kolekcję przykładów w bazie danych.
        /// </summary>

        public DbSet<Example> ExamplesDb { get; set; }
        public DbSet<Address> AddressesDb { get; set; }
        public DbSet<CategoryOfDiet> CategoryOfDietsDb { get; set; }
        public DbSet<Comment> CommentsDb { get; set; }
        public DbSet<DayWeek> DayWeeksDb { get; set; }
        public DbSet<Diet> DietsDb { get; set; }
        public DbSet<Dietician> DieticiansDb { get; set; }
        public DbSet<Diploma> DiplomasDb { get; set; }
        public DbSet<Dish> DishesDb { get; set; }
        public DbSet<FoodCatalog> FoodCatalogsDb { get; set; }
        public DbSet<Ingredient> IngridientsDb { get; set; }
        public DbSet<MealTime> MealTimesDb { get; set; }
        public DbSet<Measure> MeasuresDb { get; set; }
        public DbSet<Note> NotesDb { get; set; }
        public DbSet<Office> OfficesDb { get; set; }
        public DbSet<Patient> PatientsDb { get; set; }
        public DbSet<PatientCard> PatientCardsDb { get; set; }
        public DbSet<Rating> RatingsDb { get; set; }
        public DbSet<Recipe> RecipesDb { get; set; }
        public DbSet<SingleDiet> SingleDietsDb { get; set; }
        public DbSet<SingleTestEqual> SingleTestEqualsDb { get; set; }
        public DbSet<Status> StatusesDb { get; set; }
        public DbSet<Survey> SurveysDb { get; set; }
        public DbSet<Term> TermsDb { get; set; }
        public DbSet<TestEqual> TestEqualsDb { get; set; }
        public DbSet<Visit> VisitsDb { get; set; }
        public DbSet<Article> ArticlesDb { get; set; }
        public DbSet<Carousel> CarouselsDb { get; set; }
        public DbSet<Footer> FootersDb { get; set; }
        public DbSet<LayoutCategory> LayoutCategoriesDb { get; set; }
        public DbSet<LayoutPhoto> LayoutPhotosDb { get; set; }
        public DbSet<Link> LinksDb { get; set; }
        public DbSet<MainNavbar> NavbarsDb { get; set; }
        public DbSet<News> NewsesDb { get; set; }
        public DbSet<SocialMedia> SocialMediaDb { get; set; }
        public DbSet<SubTab> SubTabsDb { get; set; }
        public DbSet<Tab> TabsDb { get; set; }
        public DbSet<Tag> TagsDb { get; set; }
        public DbSet<CategoryType> CategoryTypesDb { get; set; }
        public DbSet<Content> ContentsDb { get; set; }
        public DbSet<Document> DocumentsDb { get; set; }
        public DbSet<FileCategory> FileCategoriesDb { get; set; }
        public DbSet<Manual> ManualsDb { get; set; }
        public DbSet<Sex> SexesDb { get; set; }
        public DbSet<MessagePatient> MessagePatientsDb { get; set; }
        public DbSet<MessageTo> MessageToDb { get; set; }
        public DbSet<Admin> AdminsDb { get; set; }
        public DbSet<MessageAdmin> MessageAdminsDb { get; set; }
        public DbSet<MessageDietetician> MessageDieteticiansDb { get; set; }
        public DbSet<DieticianPatient> DieticianPatientsDb { get; set; }

        #endregion

        #region settings
        /// <summary>
        /// Konfiguruje model, który używany jest w tworzeniu bazy danych.
        /// </summary>
        /// <param name="modelBuilder">Konstruktor modelu bazy danych.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapowanie encji i ustawienia relacji między nimi, np.:
            // Mapuje encję Patient do odpowiedniej tabeli w bazie danych.
            modelBuilder.Entity<Patient>().ToTable("PatientsDb");
            modelBuilder.Entity<Dietician>().ToTable("DieticiansDb");
            modelBuilder.Entity<Admin>().ToTable("AdminsDb");

            //**************************************************************************************************

            // Definiuje klucze podstawowe, relacje i inne konfiguracje dla pozostałych encji, np.

            // Definiuje klucz podstawowy dla encji MessagePatient.
            modelBuilder.Entity<MessagePatient>()
                .HasKey(mp => new { mp.MessageToId, mp.PatientId });

            // Ustawia relacje jeden-do-wielu pomiędzy MessageToDb i MessagePatient.
            modelBuilder.Entity<MessagePatient>()
                .HasOne(mp => mp.MessageTo)
                .WithMany(m => m.MessagePatient) // Sprawdzenie, czy MessageToDb ma kolekcję MessagePatientsDb
                .HasForeignKey(mp => mp.MessageToId);

            modelBuilder.Entity<MessagePatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(p => p.MessagePatients)  
                .HasForeignKey(mp => mp.PatientId);

            //**************************************************************************************************

            modelBuilder.Entity<MessageDietetician>()
                .HasKey(mp => new { mp.MessageToId, mp.DieticianId });

            modelBuilder.Entity<MessageDietetician>()
                .HasOne(mp => mp.MessageTo)
                .WithMany(m => m.MessageDieticians)
                .HasForeignKey(mp => mp.MessageToId);

            modelBuilder.Entity<MessageDietetician>()
                .HasOne(mp => mp.Dietician)
                .WithMany(p => p.MessageDieticians)  
                .HasForeignKey(mp => mp.DieticianId);

            //**************************************************************************************************

            modelBuilder.Entity<MessageAdmin>()
                .HasKey(mp => new { mp.MessageToId, mp.AdminId });

            modelBuilder.Entity<MessageAdmin>()
                .HasOne(mp => mp.MessageTo)
                .WithMany(m => m.MessageAdmin)
                .HasForeignKey(mp => mp.MessageToId);

            modelBuilder.Entity<MessageAdmin>()
                .HasOne(mp => mp.Admin)
                .WithMany(p => p.MessageAdmins)
                .HasForeignKey(mp => mp.AdminId);

            //**************************************************************************************************

            modelBuilder.Entity<DieticianNote>()
                .HasKey(mp => new { mp.NoteId, mp.DieticianId });

            modelBuilder.Entity<DieticianNote>()
                .HasOne(mp => mp.Note)
                .WithMany(m => m.DieticianNotes)
                .HasForeignKey(mp => mp.NoteId);

            modelBuilder.Entity<DieticianNote>()
                .HasOne(mp => mp.Dietician)
                .WithMany(p => p.DieticianNotes)  
                .HasForeignKey(mp => mp.DieticianId);

            //**************************************************************************************************
            modelBuilder.Entity<DieticianPatient>()
                .HasKey(mp => new { mp.DieticianId, mp.PatientId });

            modelBuilder.Entity<DieticianPatient>()
                .HasOne(mp => mp.Dietician)
                .WithMany(m => m.DieticianPatients)
                .HasForeignKey(mp => mp.DieticianId);

            modelBuilder.Entity<DieticianPatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(p => p.DieticianPatients)  
                .HasForeignKey(mp => mp.PatientId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DieticianOffice>()
                .HasKey(mp => new { mp.DieticianId, mp.OfficeId });

            modelBuilder.Entity<DieticianOffice>()
                .HasOne(mp => mp.Dietician)
                .WithMany(m => m.DieticianOffices)
                .HasForeignKey(mp => mp.DieticianId);

            modelBuilder.Entity<DieticianOffice>()
                .HasOne(mp => mp.Office)
                .WithMany(p => p.DieticianOffices)  
                .HasForeignKey(mp => mp.OfficeId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DietPatient>()
                .HasKey(mp => new { mp.PatientId, mp.DietId });

            modelBuilder.Entity<DietPatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(m => m.DietPatients)
                .HasForeignKey(mp => mp.PatientId);

            modelBuilder.Entity<DietPatient>()
                .HasOne(mp => mp.Diet)
                .WithMany(p => p.DietPatients)  
                .HasForeignKey(mp => mp.DietId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DietSingleDiet>()
                .HasKey(mp => new { mp.SingleDietId, mp.DietId });

            modelBuilder.Entity<DietSingleDiet>()
                .HasOne(mp => mp.SingleDiet)
                .WithMany(m => m.DietSingleDiets)
                .HasForeignKey(mp => mp.SingleDietId);

            modelBuilder.Entity<DietSingleDiet>()
                .HasOne(mp => mp.Diet)
                .WithMany(p => p.DietSingleDiets)  
                .HasForeignKey(mp => mp.DietId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DishFoodCatalog>()
                .HasKey(mp => new { mp.DishId, mp.FoodCatalogId });

            modelBuilder.Entity<DishFoodCatalog>()
                .HasOne(mp => mp.Dish)
                .WithMany(m => m.DishFoodCatalogs)
                .HasForeignKey(mp => mp.DishId);

            modelBuilder.Entity<DishFoodCatalog>()
                .HasOne(mp => mp.FoodCatalog)
                .WithMany(p => p.DishFoodCatalogs)  
                .HasForeignKey(mp => mp.FoodCatalogId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DishIngredient>()
                .HasKey(mp => new { mp.DishId, mp.IngredientId });

            modelBuilder.Entity<DishIngredient>()
                .HasOne(mp => mp.Dish)
                .WithMany(m => m.DishIngredients)
                .HasForeignKey(mp => mp.DishId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(mp => mp.Ingredient)
                .WithMany(p => p.DishIngredients)  
                .HasForeignKey(mp => mp.IngredientId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DishMeasure>()
                .HasKey(mp => new { mp.DishId, mp.MeasureId });

            modelBuilder.Entity<DishMeasure>()
                .HasOne(mp => mp.Dish)
                .WithMany(m => m.DishMeasures)
                .HasForeignKey(mp => mp.DishId);

            modelBuilder.Entity<DishMeasure>()
                .HasOne(mp => mp.Measure)
                .WithMany(p => p.DishMeasures)  
                .HasForeignKey(mp => mp.MeasureId);
            
            //**************************************************************************************************
            modelBuilder.Entity<MealTimeSingleDiet>()
                .HasKey(mp => new { mp.MealTimeId, mp.SingleDietId });

            modelBuilder.Entity<MealTimeSingleDiet>()
                .HasOne(mp => mp.MealTime)
                .WithMany(m => m.MealTimeSingleDiets)
                .HasForeignKey(mp => mp.MealTimeId);

            modelBuilder.Entity<MealTimeSingleDiet>()
                .HasOne(mp => mp.SingleDiet)
                .WithMany(p => p.MealTimeSingleDiets)  
                .HasForeignKey(mp => mp.SingleDietId);
            
            //**************************************************************************************************
            modelBuilder.Entity<NotePatient>()
                .HasKey(mp => new { mp.PatientId, mp.NoteId });

            modelBuilder.Entity<NotePatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(m => m.NotePatients)
                .HasForeignKey(mp => mp.PatientId);

            modelBuilder.Entity<NotePatient>()
                .HasOne(mp => mp.Note)
                .WithMany(p => p.NotePatients)  
                .HasForeignKey(mp => mp.NoteId);
            
            //**************************************************************************************************
            modelBuilder.Entity<PatientCardSurvey>()
                .HasKey(mp => new { mp.PatientCardId, mp.SurveyId });

            modelBuilder.Entity<PatientCardSurvey>()
                .HasOne(mp => mp.PatientCard)
                .WithMany(m => m.PatientCardSurveys)
                .HasForeignKey(mp => mp.PatientCardId);

            modelBuilder.Entity<PatientCardSurvey>()
                .HasOne(mp => mp.Survey)
                .WithMany(p => p.PatientCardSurveys)  
                .HasForeignKey(mp => mp.SurveyId);
            


        }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Konfiguracja dodatkowych opcji DbContext, np. logowania zapytań SQL.
            optionsBuilder.LogTo(item => Debug.WriteLine(item));
            base.OnConfiguring(optionsBuilder);
        }
    }

}
