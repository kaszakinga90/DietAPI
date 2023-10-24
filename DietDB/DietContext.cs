using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;
using ModelsDB.Layout;
using ModelsDB.ManualPanel;

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
<<<<<<< HEAD
        #region DbSet
        /// <summary>
        /// Pobiera lub ustawia kolekcję przykładów w bazie danych.
        /// </summary>
=======


>>>>>>> 6d047e7594153c95fdeecf218c13172cd7de5b09
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
        public DbSet<Sex> Sex { get; set; }
        public DbSet<MessagePatient> MessagePatients { get; set; }
        public DbSet<MessageToDietician> MessageToDieticians { get; set; }
        public DbSet<MessageToPatient> MessageToPatients { get; set; }

        #endregion

        #region settings
        /// <summary>
        /// Konfiguruje model, który używany jest w tworzeniu bazy danych.
        /// </summary>
        /// <param name="modelBuilder">Konstruktor modelu bazy danych.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapuje encję pacjenta do odpowiedniej tabeli w bazie danych.
            modelBuilder.Entity<Patient>().ToTable("Patients");

            // Mapuje encję dietetyka do odpowiedniej tabeli w bazie danych.
            modelBuilder.Entity<Dietician>().ToTable("Dieticians");

            //**************************************************************************************************

            // Definiuje klucz podstawowy dla encji MessagePatient.
            modelBuilder.Entity<MessagePatient>()
                .HasKey(mp => new { mp.MessageId, mp.PatientId });

            // Ustawia relacje jeden-do-wielu pomiędzy Message i MessagePatient.
            modelBuilder.Entity<MessagePatient>()
                .HasOne(mp => mp.Message)
                .WithMany(m => m.MessageToPatients) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.MessageId);

            // Ustawia relacje jeden-do-wielu pomiędzy Patient i MessagePatient.
            modelBuilder.Entity<MessagePatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(p => p.MessagePatients) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.PatientId);

            //**************************************************************************************************

            modelBuilder.Entity<DieticianMessage>()
                .HasKey(mp => new { mp.MessageId, mp.DieticianId });

            modelBuilder.Entity<DieticianMessage>()
                .HasOne(mp => mp.Message)
                .WithMany(m => m.MessageDieticians) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.MessageId);

            modelBuilder.Entity<DieticianMessage>()
                .HasOne(mp => mp.Dietician)
                .WithMany(p => p.MessageDieticians) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DieticianId);

            //**************************************************************************************************
            modelBuilder.Entity<DieticianNote>()
                .HasKey(mp => new { mp.NoteId, mp.DieticianId });

            modelBuilder.Entity<DieticianNote>()
                .HasOne(mp => mp.Note)
                .WithMany(m => m.DieticianNotes) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.NoteId);

            modelBuilder.Entity<DieticianNote>()
                .HasOne(mp => mp.Dietician)
                .WithMany(p => p.DieticianNotes) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DieticianId);

            //**************************************************************************************************
            modelBuilder.Entity<DieticianPatient>()
                .HasKey(mp => new { mp.DieticianId, mp.PatientId });

            modelBuilder.Entity<DieticianPatient>()
                .HasOne(mp => mp.Dietician)
                .WithMany(m => m.DieticianPatients) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DieticianId);

            modelBuilder.Entity<DieticianPatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(p => p.DieticianPatients) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.PatientId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DieticianOffice>()
                .HasKey(mp => new { mp.DieticianId, mp.OfficeId });

            modelBuilder.Entity<DieticianOffice>()
                .HasOne(mp => mp.Dietician)
                .WithMany(m => m.DieticianOffices) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DieticianId);

            modelBuilder.Entity<DieticianOffice>()
                .HasOne(mp => mp.Office)
                .WithMany(p => p.DieticianOffices) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.OfficeId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DietPatient>()
                .HasKey(mp => new { mp.PatientId, mp.DietId });

            modelBuilder.Entity<DietPatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(m => m.DietPatients) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.PatientId);

            modelBuilder.Entity<DietPatient>()
                .HasOne(mp => mp.Diet)
                .WithMany(p => p.DietPatients) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DietId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DietSingleDiet>()
                .HasKey(mp => new { mp.SingleDietId, mp.DietId });

            modelBuilder.Entity<DietSingleDiet>()
                .HasOne(mp => mp.SingleDiet)
                .WithMany(m => m.DietSingleDiets) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.SingleDietId);

            modelBuilder.Entity<DietSingleDiet>()
                .HasOne(mp => mp.Diet)
                .WithMany(p => p.DietSingleDiets) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DietId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DishFoodCatalog>()
                .HasKey(mp => new { mp.DishId, mp.FoodCatalogId });

            modelBuilder.Entity<DishFoodCatalog>()
                .HasOne(mp => mp.Dish)
                .WithMany(m => m.DishFoodCatalogs) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DishId);

            modelBuilder.Entity<DishFoodCatalog>()
                .HasOne(mp => mp.FoodCatalog)
                .WithMany(p => p.DishFoodCatalogs) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.FoodCatalogId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DishIngredient>()
                .HasKey(mp => new { mp.DishId, mp.IngredientId });

            modelBuilder.Entity<DishIngredient>()
                .HasOne(mp => mp.Dish)
                .WithMany(m => m.DishIngredients) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DishId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(mp => mp.Ingredient)
                .WithMany(p => p.DishIngredients) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.IngredientId);
            
            //**************************************************************************************************
            modelBuilder.Entity<DishMeasure>()
                .HasKey(mp => new { mp.DishId, mp.MeasureId });

            modelBuilder.Entity<DishMeasure>()
                .HasOne(mp => mp.Dish)
                .WithMany(m => m.DishMeasures) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.DishId);

            modelBuilder.Entity<DishMeasure>()
                .HasOne(mp => mp.Measure)
                .WithMany(p => p.DishMeasures) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.MeasureId);
            
            //**************************************************************************************************
            modelBuilder.Entity<MealTimeSingleDiet>()
                .HasKey(mp => new { mp.MealTimeId, mp.SingleDietId });

            modelBuilder.Entity<MealTimeSingleDiet>()
                .HasOne(mp => mp.MealTime)
                .WithMany(m => m.MealTimeSingleDiets) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.MealTimeId);

            modelBuilder.Entity<MealTimeSingleDiet>()
                .HasOne(mp => mp.SingleDiet)
                .WithMany(p => p.MealTimeSingleDiets) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.SingleDietId);
            
            //**************************************************************************************************
            modelBuilder.Entity<NotePatient>()
                .HasKey(mp => new { mp.PatientId, mp.NoteId });

            modelBuilder.Entity<NotePatient>()
                .HasOne(mp => mp.Patient)
                .WithMany(m => m.NotePatients) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.PatientId);

            modelBuilder.Entity<NotePatient>()
                .HasOne(mp => mp.Note)
                .WithMany(p => p.NotePatients) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.NoteId);
            
            //**************************************************************************************************
            modelBuilder.Entity<PatientCardSurvey>()
                .HasKey(mp => new { mp.PatientCardId, mp.SurveyId });

            modelBuilder.Entity<PatientCardSurvey>()
                .HasOne(mp => mp.PatientCard)
                .WithMany(m => m.PatientCardSurveys) // Upewnij się, że Message ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.PatientCardId);

            modelBuilder.Entity<PatientCardSurvey>()
                .HasOne(mp => mp.Survey)
                .WithMany(p => p.PatientCardSurveys) // Upewnij się, że Patient ma kolekcję MessagePatients
                .HasForeignKey(mp => mp.SurveyId);
            


        }
        #endregion
    }

}
