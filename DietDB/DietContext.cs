using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class DietContext : IdentityDbContext<User, Role, int>
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
        public DbSet<DishIngredient> DishIngredientsDb { get; set; }
        public DbSet<MealTimeToXYAxis> MealTimesDb { get; set; }
        public DbSet<Measure> MeasuresDb { get; set; }
        public DbSet<Note> NotesDb { get; set; }
        public DbSet<Office> OfficesDb { get; set; }
        public DbSet<Patient> PatientsDb { get; set; }
        public DbSet<PatientCard> PatientCardsDb { get; set; }
        public DbSet<Rating> RatingsDb { get; set; }
        public DbSet<Recipe> RecipesDb { get; set; }
        public DbSet<RecipeStep> RecipeStepsDb { get; set; }
        public DbSet<SingleTestResults> SingleTestResultsDb { get; set; }
        public DbSet<Status> StatusesDb { get; set; }
        public DbSet<Survey> SurveysDb { get; set; }
        public DbSet<Term> TermsDb { get; set; }
        public DbSet<TestResult> TestResultsDb { get; set; }
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
        public DbSet<Nutrient> NutrientsDb { get; set; }
        public DbSet<IngredientNutrient> IngredientNutrientsDb { get; set; }
        public DbSet<Ingredient> IngredientsDb { get; set; }
        public DbSet<Unit> UnitsDb { get; set; }
        public DbSet<Specialization> SpecializationsDb { get; set; }
        public DbSet<DieticianSpecialization> DieticianSpecialization { get; set; }
        public DbSet<PatientCardSurvey> PatientCardSurveysDb { get; set; }
        public DbSet<DieticianPatientRating> DieticianPatientRatings { get; set; }
        public DbSet<Logo> LogosDb { get; set; }
        
        public DbSet<DieticianOffice> DieticianOffices { get; set; }
        public DbSet<ReportTemplate> ReportTemplatesDb { get; set; }

        public DbSet<Meal> MealsDb { get; set; }
        //public DbSet<Role> Role { get; set; }
        //public DbSet<MealTimeToXYAxis> MealTimeToXYAxis { get; set; }


        #endregion

        #region settings
        /// <summary>
        /// Konfiguruje model, który używany jest w tworzeniu bazy danych.
        /// </summary>
        /// <param name="modelBuilder">Konstruktor modelu bazy danych.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.FirstName, u.LastName })
                .HasDatabaseName("IX_FirstNameLastName");
            modelBuilder.Entity<Ingredient>()
                .HasIndex(i => new { i.Name })
                .HasDatabaseName("IX_IngredientName");


            modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Patient", NormalizedName = "PATIENT" },
                new IdentityRole { Name = "Dietetician", NormalizedName = "DIETETICIAN" }
                );
            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.HasKey(login => new { login.ProviderKey, login.LoginProvider });
            });
            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
            });
            modelBuilder.Entity<IdentityUserToken<int>>(b =>
            {
                b.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
            });
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

            modelBuilder.Entity<TestResult>()
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
            modelBuilder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId)
                .OnDelete(DeleteBehavior.Restrict); // Specify the desired behavior

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId)
                .OnDelete(DeleteBehavior.Restrict); // Specify the desired behavior

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
            //    .HasForeignKey(mp => mp.PatientId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<DietPatient>()
            //    .HasOne(mp => mp.Diet)
            //    .WithMany(p => p.diet)
            //    .HasForeignKey(mp => mp.DietId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<DieticianSpecialization>()
                .HasKey(np => new { np.DieticianId, np.SpecializationId });

            modelBuilder.Entity<DieticianSpecialization>()
                .HasOne(np => np.Dietician)
                .WithMany(p => p.DieticianSpecializations)
                .HasForeignKey(np => np.DieticianId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DieticianSpecialization>()
                .HasOne(np => np.Specialization)
                .WithMany(n => n.DieticianSpecializations)
                .HasForeignKey(np => np.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<Recipe>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Dish)
                .WithOne(d => d.Recipe)
                .HasForeignKey<Dish>(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<PatientCard>()
                .HasOne(pc => pc.Patient)
                .WithMany(p => p.PatientCards)
                .HasForeignKey(pc => pc.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<DieticianPatientRating>()
                .HasKey(dpr => new { dpr.DieticianId, dpr.PatientId });

            modelBuilder.Entity<DieticianPatientRating>()
                .HasOne(dpr => dpr.Dietician)
                .WithMany(d => d.DieticianRatings)
                .HasForeignKey(dpr => dpr.DieticianId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DieticianPatientRating>()
                .HasOne(dpr => dpr.Patient)
                .WithMany(p => p.DieticianRatings)
                .HasForeignKey(dpr => dpr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DieticianPatientRating>()
                .HasOne(dpr => dpr.Rating)
                .WithMany(r => r.DieticianPatientRatings)
                .HasForeignKey(dpr => dpr.RatingId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------------------------------------------------------------------- //

            modelBuilder.Entity<Logo>()
                .HasOne(l => l.Dietician)
                .WithOne(d => d.Logo)
                .HasForeignKey<Logo>(l => l.DieticianId);

            modelBuilder.Entity<Dietician>()
                .HasOne(d => d.Logo)
                .WithOne(l => l.Dietician)
                .HasForeignKey<Logo>(l => l.DieticianId)
                .IsRequired(false);


        }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Konfiguracja dodatkowych opcji DbContext, np. logowania zapytań SQL.
            optionsBuilder.LogTo(item => Debug.WriteLine(item));
            //optionsBuilder.ConfigureWarnings(w => w.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
            base.OnConfiguring(optionsBuilder);
        }
    }

}
