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
        public DbSet<MealTimeToXYAxis> MealTimesDb { get; set; }
        public DbSet<Measure> MeasuresDb { get; set; }
        public DbSet<Note> NotesDb { get; set; }
        public DbSet<Office> OfficesDb { get; set; }
        public DbSet<Patient> PatientsDb { get; set; }
        public DbSet<PatientCard> PatientCardsDb { get; set; }
        public DbSet<Rating> RatingsDb { get; set; }
        public DbSet<Recipe> RecipesDb { get; set; }
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
        public DbSet<MessageTo> MessageToDb { get; set; }
        public DbSet<Admin> AdminsDb { get; set; }
        public DbSet<DieticianPatient> DieticianPatientsDb { get; set; }
        public DbSet<MealSchedule> MealSchedulesDb { get; set; }
        public DbSet<Nutrient> NutrientsDb { get; set; }
        public DbSet<IngredientNutrient> IngredientNutrientsDb { get; set; }
        public DbSet<Ingredient> IngredientsDb { get; set; }
        public DbSet<Unit> UnitsDb { get; set; }

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

            //*************************************** Restrict Mode on Delete ***********************************************************

            modelBuilder.Entity<Link>()
                .HasOne(link => link.SocialMedia)
                .WithOne(socialMedia => socialMedia.Link)
                .HasForeignKey<SocialMedia>(socialMedia => socialMedia.LinkId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<DieticianOffice>()
                .HasKey(mp => new { mp.DieticianId, mp.OfficeId });

            modelBuilder.Entity<DieticianOffice>()
                .HasOne(mp => mp.Dietician)
                .WithMany(m => m.DieticianOffices)
                .HasForeignKey(mp => mp.DieticianId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DieticianOffice>()
                .HasOne(mp => mp.Office)
                .WithMany(p => p.DieticianOffices)
                .HasForeignKey(mp => mp.OfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<DieticianPatient>()
                .HasKey(dp => new { dp.PatientId, dp.DieticianId });

            modelBuilder.Entity<DieticianPatient>()
                .HasOne(dp => dp.Patient)
                .WithMany(p => p.DieticianPatients)
                .HasForeignKey(dp => dp.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DieticianPatient>()
                .HasOne(dp => dp.Dietician)
                .WithMany(d => d.DieticianPatients)
                .HasForeignKey(dp => dp.DieticianId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<NotePatient>()
                .HasKey(np => new { np.PatientId, np.NoteId });

            modelBuilder.Entity<NotePatient>()
                .HasOne(np => np.Patient)
                .WithMany(p => p.NotePatients)
                .HasForeignKey(np => np.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NotePatient>()
                .HasOne(np => np.Note)
                .WithMany(n => n.NotePatients)
                .HasForeignKey(np => np.NoteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<DieticianNote>()
                .HasKey(dn => new { dn.DieticianId, dn.NoteId });

            modelBuilder.Entity<DieticianNote>()
                .HasOne(dn => dn.Note)
                .WithMany(n => n.DieticianNotes)
                .HasForeignKey(dn => dn.NoteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<TestEqual>()
                .HasOne(te => te.Patient)
                .WithMany(p => p.TestEquals)
                .HasForeignKey(te => te.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Patient)
                .WithMany(p => p.Visits)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<IngredientNutrient>()
                 .HasOne(inut => inut.Nutrient)
                 .WithMany(n => n.Ingredients)
                 .HasForeignKey(inut => inut.NutrientId)
                 .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<Diet>()
                .HasOne(d => d.Patient)
                .WithMany(p => p.Diets)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            //**************************************************************************************************

            modelBuilder.Entity<Diet>()
                .HasOne(d => d.Dietician) // Diet ma jeden Dietician
                .WithMany(di => di.Diets) // Dietician ma wiele Diet
                .HasForeignKey(d => d.DieteticianId); // Klucz obcy w Diet to DieteticianId

            //**************************************************************************************************

            //modelBuilder.Entity<DishFoodCatalog>()
            //    .HasKey(mp => new { mp.DishId, mp.FoodCatalogId });

            //modelBuilder.Entity<DishFoodCatalog>()
            //    .HasOne(mp => mp.Dish)
            //    .WithMany(m => m.DishFoodCatalogs)
            //    .HasForeignKey(mp => mp.DishId);

            //modelBuilder.Entity<DishFoodCatalog>()
            //    .HasOne(mp => mp.FoodCatalog)
            //    .WithMany(p => p.DishFoodCatalogs)  
            //    .HasForeignKey(mp => mp.FoodCatalogId);
            
            //**************************************************************************************************
            //modelBuilder.Entity<DishIngredient>()
            //    .HasKey(mp => new { mp.DishId, mp.IngredientId });

            //modelBuilder.Entity<DishIngredient>()
            //    .HasOne(mp => mp.Dish)
            //    .WithMany(m => m.DishIngredients)
            //    .HasForeignKey(mp => mp.DishId);

            //modelBuilder.Entity<DishIngredient>()
            //    .HasOne(mp => mp.Ingredient)
            //    .WithMany(p => p.DishIngredients)  
            //    .HasForeignKey(mp => mp.IngredientId);
            
            //**************************************************************************************************
            //modelBuilder.Entity<DishMeasure>()
            //    .HasKey(mp => new { mp.DishId, mp.MeasureId });

            //modelBuilder.Entity<DishMeasure>()
            //    .HasOne(mp => mp.Dish)
            //    .WithMany(m => m.DishMeasures)
            //    .HasForeignKey(mp => mp.DishId);

            //modelBuilder.Entity<DishMeasure>()
            //    .HasOne(mp => mp.Measure)
            //    .WithMany(p => p.DishMeasures)  
            //    .HasForeignKey(mp => mp.MeasureId);
            
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
            //**************************************************************************************************
            modelBuilder.Entity<IngredientNutrient>()
                .HasKey(inr => new { inr.IngredientId, inr.NutrientId });

            modelBuilder.Entity<IngredientNutrient>()
                .HasOne(inr => inr.Ingredient)
                .WithMany(i => i.Nutrients)
                .HasForeignKey(inr => inr.IngredientId);

            modelBuilder.Entity<IngredientNutrient>()
                .HasOne(inr => inr.Nutrient)
                .WithMany(n => n.Ingredients)
                .HasForeignKey(inr => inr.NutrientId);

            //modelBuilder.Entity<DietPatient>()
            //    .HasKey(mp => new { mp.PatientId, mp.DietId });

            //modelBuilder.Entity<DietPatient>()
            //    .HasOne(mp => mp.Patient)
            //    .WithMany(m => m.DietPatients)
            //    .HasForeignKey(mp => mp.PatientId);

            //modelBuilder.Entity<DietPatient>()
            //    .HasOne(mp => mp.Diet)
            //    .WithMany(p => p.DietPatients)  
            //    .HasForeignKey(mp => mp.DietId);

        }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Konfiguracja dodatkowych opcji DbContext, np. logowania zapytań SQL.
            optionsBuilder.LogTo(item => Debug.WriteLine(item))
                .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }

}
