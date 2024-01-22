using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelsDB;
using ModelsDB.Functionality;

namespace DietDB
{
    public class Seed
    {
        public static async Task SeedData(DietContext context, UserManager<User> userManager)
        {
            await SeedBaseClasses(context);
            await SeedFirstClassesWithForeignKey(context, userManager);
            await SeedSecondClassesWithForeignKey(context);
            await SeedThirdClassesWithForeignKey(context);
            await ExecuteSqlCommands(context);

            await context.SaveChangesAsync();
        }

        // ******************** DANE PODSTAWOWE **************************************** //
        private static async Task SeedBaseClasses(DietContext context)
        {
            #region ReportTemplates
            // Sprawdzanie i dodawanie testowych reportTemplates
            if (!context.ReportTemplatesDb.Any())
            {
                var rt = new List<ReportTemplate>()
                {
                    new ReportTemplate { Name = "Szablon1", isContainHeader = true, isContainFooter = false },
                    new ReportTemplate { Name = "Szablon2", isContainHeader = false, isContainFooter = true },
                    new ReportTemplate { Name = "Szablon3", isContainHeader = true, isContainFooter = true },
                };
                await context.ReportTemplatesDb.AddRangeAsync(rt);
            }
            #endregion

            #region ReportTemplatePreview
            // Sprawdzanie i dodawanie testowych ReportTemplatePreviews
            if (!context.ReportTemplatePreviewsDb.Any())
            {
                var rtp = new List<ReportTemplatePreview>()
                {
                    new ReportTemplatePreview { UrlReportTemplate1 = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705600613/s8gyybsntm1lcycjkwpa.png", 
                                                UrlReportTemplate2 = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705600668/rcofw17lkgv8hane2uh2.png", 
                                                UrlReportTemplate3 = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705600711/cct5y7cxoacbtdh2sfx2.png",
                                                UrlReportTemplate4 = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705600797/x4ckvnj5dr1wh1o3v653.png"
                    },
                };
                await context.ReportTemplatePreviewsDb.AddRangeAsync(rtp);
            }
            #endregion

            #region CountryState
            if (!context.CountryStatesDb.Any())
            {
                // Lista województw w Polsce
                var states = new List<CountryState>()
            {
                new CountryState { StateName = "-- uzupelnij dane --" },
                new CountryState { StateName = "Dolnośląskie" },
                new CountryState { StateName = "Kujawsko-Pomorskie" },
                new CountryState { StateName = "Lubelskie" },
                new CountryState { StateName = "Lubuskie" },
                new CountryState { StateName = "Łódzkie" },
                new CountryState { StateName = "Małopolskie" },
                new CountryState { StateName = "Mazowieckie" },
                new CountryState { StateName = "Opolskie" },
                new CountryState { StateName = "Podkarpackie" },
                new CountryState { StateName = "Podlaskie" },
                new CountryState { StateName = "Pomorskie" },
                new CountryState { StateName = "Śląskie" },
                new CountryState { StateName = "Świętokrzyskie" },
                new CountryState { StateName = "Warmińsko-Mazurskie" },
                new CountryState { StateName = "Wielkopolskie" },
                new CountryState { StateName = "Zachodniopomorskie" }
            };

                await context.CountryStatesDb.AddRangeAsync(states);
            }
            #endregion
            #region CategoryOfDiet
            // Sprawdzanie i dodawanie testowych kategorii diet
            if (!context.CategoryOfDietsDb.Any())
            {
                var categoriesOfDiets = new List<CategoryOfDiet>()
                {
                    new CategoryOfDiet { CategoryName = "Wegetariańska" },
                    new CategoryOfDiet { CategoryName = "Wegańska" },
                    new CategoryOfDiet { CategoryName = "Keto" },
                    new CategoryOfDiet { CategoryName = "Paleo" },
                    new CategoryOfDiet { CategoryName = "Śródziemnomorska" },
                    new CategoryOfDiet { CategoryName = "Niska zawartość węglowodanów" },
                    new CategoryOfDiet { CategoryName = "Wysokobiałkowa" },
                    new CategoryOfDiet { CategoryName = "Bezglutenowa" },
                    new CategoryOfDiet { CategoryName = "Bezlaktozowa" },
                    new CategoryOfDiet { CategoryName = "Surowa" },
                };
                await context.CategoryOfDietsDb.AddRangeAsync(categoriesOfDiets);
            }
            #endregion
            #region DayWeek
            // Sprawdzanie i dodawanie danych dla dni tygodnia w DayWeeksDb
            if (!context.DayWeeksDb.Any())
            {
                var days = new List<DayWeek>()
                {
                    new DayWeek { Day = "Poniedziałek" },
                    new DayWeek { Day = "Wtorek" },
                    new DayWeek { Day = "Środa" },
                    new DayWeek { Day = "Czwartek" },
                    new DayWeek { Day = "Piątek" },
                    new DayWeek { Day = "Sobota" },
                    new DayWeek { Day = "Niedziela" }
                };
                await context.DayWeeksDb.AddRangeAsync(days);
            }
            #endregion
            #region Sex
            // Sprawdzanie i dodawanie testowych danych dla SexesDb
            if (!context.SexesDb.Any())
            {
                var sexes = new List<Sex>()
                {
                    new Sex { Name = "Mężczyzna" },
                    new Sex { Name = "Kobieta" },
                };
                await context.SexesDb.AddRangeAsync(sexes);
            }
            #endregion
            #region Roles
            if (!context.Roles.Any())
            {
                var roles = new List<Role>()
                {
                    new Role{Name="SuperAdmin", NormalizedName="SUPERADMIN"},
                    new Role{Name="Admin", NormalizedName="ADMIN"},
                    new Role{Name="Patient", NormalizedName="PATIENT"},
                    new Role{Name="Dietetician", NormalizedName="DIETETICIAN"},
                };
                await context.Roles.AddRangeAsync(roles);
            }
            #endregion
            #region Measure
            // Sprawdzanie i dodawanie testowych jednostek miary
            if (!context.MeasuresDb.Any())
            {
                var measures = new List<Measure>()
                {
                    new Measure { Symbol = "Piece", Description = "Each" },
                    new Measure { Symbol = "Cup", Description = "Cup" },
                    new Measure { Symbol = "Teaspoon", Description = "Teaspoon" },
                    new Measure { Symbol = "Tablespoon", Description = "Tablespoon" },
                    new Measure { Symbol = "Ounce", Description = "Ounce" },
                };
                await context.MeasuresDb.AddRangeAsync(measures);
            }
            #endregion
            #region Rating
            // Sprawdzanie i dodawanie testowych ocen
            if (!context.RatingsDb.Any())
            {
                var ratings = new List<Rating>()
                {
                    new Rating { Note = 5 }, // 5/5 - doskonała ocena
                    new Rating { Note = 4 }, // 4/5 - bardzo dobra ocena
                    new Rating { Note = 3 }, // 3/5 - średnia ocena
                    new Rating { Note = 2 }, // 2/5 - poniżej średniej oceny
                    new Rating { Note = 1 }, // 1/5 - niska ocena
                };
                await context.RatingsDb.AddRangeAsync(ratings);
            }
            #endregion
            #region Survey
            // Sprawdzanie i dodawanie testowych rekordów dla Survey
            if (!context.SurveysDb.Any())
            {
                var surveys = new List<Survey>()
                {
                    new Survey { Heigth = 175.5f, Weith = 70.2f, MeasureTime = DateTime.Now.AddDays(-10) },
                    new Survey { Heigth = 160.4f, Weith = 60.1f, MeasureTime = DateTime.Now.AddDays(-9) },
                    new Survey { Heigth = 182.7f, Weith = 80.5f, MeasureTime = DateTime.Now.AddDays(-8) },
                    new Survey { Heigth = 168.2f, Weith = 65.3f, MeasureTime = DateTime.Now.AddDays(-7) },
                    new Survey { Heigth = 170.0f, Weith = 72.4f, MeasureTime = DateTime.Now.AddDays(-6) },
                    new Survey { Heigth = 177.8f, Weith = 74.2f, MeasureTime = DateTime.Now.AddDays(-5) },
                    new Survey { Heigth = 174.3f, Weith = 68.9f, MeasureTime = DateTime.Now.AddDays(-4) },
                    new Survey { Heigth = 162.6f, Weith = 63.7f, MeasureTime = DateTime.Now.AddDays(-3) },
                    new Survey { Heigth = 179.1f, Weith = 76.0f, MeasureTime = DateTime.Now.AddDays(-2) },
                    new Survey { Heigth = 165.5f, Weith = 62.5f, MeasureTime = DateTime.Now.AddDays(-1) },
                };
                await context.SurveysDb.AddRangeAsync(surveys);
            }
            #endregion
            #region Term
            // Sprawdzanie i dodawanie testowych rekordów dla Term
            if (!context.TermsDb.Any())
            {
                var terms = new List<Term>()
                {
                    new Term { Name = "Poranny" },
                    new Term { Name = "Przedpołudniowy" },
                    new Term { Name = "Popołudniowy" },
                    new Term { Name = "Wieczorny" },
                    new Term { Name = "Nocny" },
                    new Term { Name = "Świąteczny" },
                    new Term { Name = "Weekendowy" },
                    new Term { Name = "Specjalny" },
                    new Term { Name = "Długi weekend" },
                    new Term { Name = "Ekspresowy" }
                };
                await context.TermsDb.AddRangeAsync(terms);
            }
            #endregion
            #region Unit
            // Sprawdzanie i dodawanie testowych rekordów dla Unit
            if (!context.UnitsDb.Any())
            {
                var units = new List<Unit>()
                {
                    new Unit { Symbol = "g", Description = "gram" },
                    new Unit { Symbol = "mg", Description = "miligram" },
                    new Unit { Symbol = "kcal", Description = "kalorie" },
                    new Unit { Symbol = "IU", Description = "jednostka międzynarodowa" },
                    new Unit { Symbol = "Âµg", Description = "mikrogram" },
                    new Unit { Symbol = "kJ", Description = "kilojoule" },
                    new Unit { Symbol = "kg", Description = "kilogram" },
                    new Unit { Symbol = "ml", Description = "mililitr" },
                    new Unit { Symbol = "l", Description = "litr" }
                };
                await context.UnitsDb.AddRangeAsync(units);
            }
            #endregion
            #region Specialization
            // Dodawanie danych dla specjalizacji dietetycznych w SpecializationsDb
            if (!context.SpecializationsDb.Any())
            {
                var specializations = new List<Specialization>()
                {
                    new Specialization { SpecializationName = "Dietetyka kliniczna" },
                    new Specialization { SpecializationName = "Dietetyka sportowa" },
                    new Specialization { SpecializationName = "Dietetyka dziecięca" },
                    new Specialization { SpecializationName = "Dietetyka w cukrzycy" },
                    new Specialization { SpecializationName = "Dietetyka w chorobach serca" },
                    new Specialization { SpecializationName = "Dietetyka w chorobach nerek" },
                    new Specialization { SpecializationName = "Dietetyka w chorobach przewodu pokarmowego" },
                    new Specialization { SpecializationName = "Dietetyka onkologiczna" },
                    new Specialization { SpecializationName = "Dietetyka geriatryczna" },
                    new Specialization { SpecializationName = "Dietetyka w zaburzeniach odżywiania" },
                    new Specialization { SpecializationName = "Dietetyka wegetariańska" },
                    new Specialization { SpecializationName = "Dietetyka wegańska" },
                    new Specialization { SpecializationName = "Dietetyka w alergiach pokarmowych" },
                    new Specialization { SpecializationName = "Dietetyka w nietolerancjach pokarmowych" },
                    new Specialization { SpecializationName = "Dietetyka w chorobach autoimmunologicznych" },
                    new Specialization { SpecializationName = "Dietetyka w zaburzeniach metabolicznych" },
                    new Specialization { SpecializationName = "Dietetyka w menopauzie" },
                    new Specialization { SpecializationName = "Dietetyka w ciąży i laktacji" },
                    new Specialization { SpecializationName = "Dietetyka w zdrowiu psychicznym" },
                    new Specialization { SpecializationName = "Dietetyka w geriatrii" }
                };
                await context.SpecializationsDb.AddRangeAsync(specializations);
            }
            #endregion
            #region Meals
            // Sprawdzanie i dodawanie testowych danych dla Meal
            if (!context.MealsDb.Any())
            {
                var meals = new List<Meal>()
                {
                    new Meal { Name = "Śniadanie" },
                    new Meal { Name = "II Śniadanie" },
                    new Meal { Name = "Obiad" },
                    new Meal { Name = "Podwieczorek" },
                    new Meal { Name = "Kolacja" }
                };
                await context.MealsDb.AddRangeAsync(meals);
            }
            #endregion

            await context.SaveChangesAsync();
        }

        // ******************** DANE Z KLUCZAMI OBCYMI **************************************** //
        private static async Task SeedFirstClassesWithForeignKey(DietContext context, UserManager<User> userManager)
        {
            #region Address
            // Sprawdzanie i dodawanie testowych adresów
            if (!context.AddressesDb.Any())
            {
                var addresses = new List<Address>()
                {
                    new Address { City = "Warszawa", CountryStateId = 2, ZipCode = "00-001", Country = "Polska", Street = "Marszałkowska", LocalNo = "24A" },
                    new Address { City = "Kraków", CountryStateId = 3, ZipCode = "30-002", Country = "Polska", Street = "Floriańska", LocalNo = "15B" },
                    new Address { City = "Wrocław", CountryStateId = 3, ZipCode = "50-002", Country = "Polska", Street = "Rynek", LocalNo = "12C" },
                    new Address { City = "Poznań", CountryStateId = 9, ZipCode = "60-002", Country = "Polska", Street = "Stawna", LocalNo = "8D" },
                    new Address { City = "Gdańsk", CountryStateId = 5, ZipCode = "80-002", Country = "Polska", Street = "Długa", LocalNo = "5E" },
                    new Address { City = "Lublin", CountryStateId = 5, ZipCode = "20-001", Country = "Polska", Street = "Lipowa", LocalNo = "3F" },
                    new Address { City = "Katowice", CountryStateId = 9, ZipCode = "40-001", Country = "Polska", Street = "Mariacka", LocalNo = "2G" },
                    new Address { City = "Bydgoszcz", CountryStateId = 10, ZipCode = "85-001", Country = "Polska", Street = "Długa", LocalNo = "6H" },
                    new Address { City = "Białystok", CountryStateId = 13, ZipCode = "15-001", Country = "Polska", Street = "Lipowa", LocalNo = "9I" },
                    new Address { City = "Rzeszów", CountryStateId = 8, ZipCode = "35-001", Country = "Polska", Street = "3 Maja", LocalNo = "4J" },
                    //Office addresses
                    new Address { City = "Kraków", CountryStateId = 7, ZipCode = "30-011", Country = "Polska", Street = "Floriańska", LocalNo = "15A" },
                    new Address { City = "Kraków", CountryStateId = 7, ZipCode = "30-011", Country = "Polska", Street = "Marszałkowska", LocalNo = "27B" },
                    new Address { City = "Gdańsk", CountryStateId = 17, ZipCode = "80-802", Country = "Polska", Street = "Długa", LocalNo = "12/5" },
                    new Address { City = "Katowice", CountryStateId = 13, ZipCode = "40-001", Country = "Polska", Street = "Oławska", LocalNo = "33" },
                    new Address { City = "Katowice", CountryStateId = 13, ZipCode = "40-001", Country = "Polska", Street = "Półwiejska", LocalNo = "20/7" }
                };
                await context.AddressesDb.AddRangeAsync(addresses);
            }
            #endregion
            #region Users
            if (!userManager.Users.Any())
            {
                var superAdmin = new Admin
                {
                    UserName = "darth.vader@example.com",
                    FirstName = "Darth",
                    LastName = "Vader",
                    Email = "darth.vader@example.com",
                    PhoneNumber = "1111111111",
                    isPatient = false,
                    isDietician = false,
                    isAdmin = true,
                    isSuperAdmin = true,
                    BirthDate = new DateTime(1970, 10, 12),
                    AddressId = 8,
                    EmailConfirmed = true,
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705234573/qc1pclrxojm3arwfkrif.jpg"
                };
                await userManager.CreateAsync(superAdmin, "Pa$$w0rd5555555554!");

                var admin = new Admin
                {
                    UserName = "jannzz.kowalski@example.com",
                    FirstName = "Janusz",
                    LastName = "Kowalski",
                    Email = "jannzz.kowalski@example.com",
                    PhoneNumber = "500100200",
                    isPatient = false,
                    isDietician = false,
                    isAdmin = true,
                    isSuperAdmin = false,
                    BirthDate = new DateTime(1980, 1, 1),
                    AddressId = 1,
                    EmailConfirmed = true,
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705234573/qc1pclrxojm3arwfkrif.jpg"
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd5555555554!");

                var admin1 = new Admin
                {
                    UserName = "ewa.nowak@example.com",
                    FirstName = "Ewa",
                    LastName = "Nowak",
                    Email = "ewa.nowak@example.com",
                    PhoneNumber = "500100201",
                    isPatient = false,
                    isDietician = false,
                    isAdmin = true,
                    isSuperAdmin = false,
                    BirthDate = new DateTime(1982, 5, 10),
                    AddressId = 2,
                    EmailConfirmed = true,
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1705234573/qc1pclrxojm3arwfkrif.jpg"
                };
                await userManager.CreateAsync(admin1, "Pa$$w0rd5555555554!");

                //***********************************************PATIENT************************
                var patient1 = new Patient
                {
                    UserName = "tomasz.zielinski@example.com",
                    FirstName = "Tomasz",
                    LastName = "Zieliński",
                    Email = "tomasz.zielinski@example.com",
                    PhoneNumber = "500600700",
                    isPatient = true,
                    isDietician = false,
                    isAdmin = false,
                    BirthDate = new DateTime(1985, 6, 15),
                    AddressId = 3,
                    EmailConfirmed = true,
                    PublicId = "uamsvfuzxfdw64jetcpg",
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704441212/uamsvfuzxfdw64jetcpg.jpg",
                    //PatientCardId = 1,
                };
                await userManager.CreateAsync(patient1, "Pa$$w0rd5555555554!");

                var patient2 = new Patient
                {
                    UserName = "aleksandra.nowak@example.com",
                    FirstName = "Aleksandra",
                    LastName = "Nowak",
                    Email = "aleksandra.nowak@example.com",
                    PhoneNumber = "501601701",
                    isPatient = true,
                    isDietician = false,
                    isAdmin = false,
                    BirthDate = new DateTime(1990, 2, 20),
                    AddressId = 4,
                    EmailConfirmed = true,
                    PublicId = "wxb3pnjaiknxg96v46ck",
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704441271/wxb3pnjaiknxg96v46ck.jpg",
                    //PatientCardId = 2,
                };
                await userManager.CreateAsync(patient2, "Pa$$w0rd5555555554!");

                var patient3 = new Patient
                {
                    UserName = "piotr.kowal@example.com",
                    FirstName = "Piotr",
                    LastName = "Kowal",
                    Email = "piotr.kowal@example.com",
                    PhoneNumber = "502602702",
                    isPatient = true,
                    isDietician = false,
                    isAdmin = false,
                    BirthDate = new DateTime(1988, 5, 5),
                    AddressId = 5,
                    EmailConfirmed = true,
                    PublicId = "vg3nmqdvdho3pbwwhpkc",
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704441318/vg3nmqdvdho3pbwwhpkc.jpg",
                    //PatientCardId = 3,
                };
                await userManager.CreateAsync(patient3, "Pa$$w0rd5555555554!");

                //*****************************DIETICIAN******************************************
                var dietician1 = new Dietician
                {
                    UserName = "anna.nowak1@example.com",
                    FirstName = "Anna",
                    LastName = "Nowak",
                    Email = "anna.nowak1@example.com",
                    PhoneNumber = "987654321",
                    isPatient = false,
                    isDietician = true,
                    isAdmin = false,
                    BirthDate = new DateTime(1985, 2, 2),
                    AddressId = 6,
                    EmailConfirmed = true,
                    PublicId = "z2ljp7qhkuis0nwdpohf",
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273249/z2ljp7qhkuis0nwdpohf.jpg",
                    //RatingId = 1
                };
                await userManager.CreateAsync(dietician1, "Pa$$w0rd5555555554!");

                var dietician2 = new Dietician
                {
                    UserName = "marek.kowalski@example.com",
                    FirstName = "Marek",
                    LastName = "Kowalski",
                    Email = "marek.kowalski@example.com",
                    PhoneNumber = "987654322",
                    isPatient = false,
                    isDietician = true,
                    isAdmin = false,
                    BirthDate = new DateTime(1984, 3, 3),
                    AddressId = 7,
                    EmailConfirmed = true,
                    PublicId = "cbjoqw8sf59dwg06p6vt",
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273373/cbjoqw8sf59dwg06p6vt.jpg",
                    //RatingId = 2
                };
                await userManager.CreateAsync(dietician2, "Pa$$w0rd5555555554!");

                var dietician3 = new Dietician
                {
                    UserName = "oliwia.wisniewska@example.com",
                    FirstName = "Oliwia",
                    LastName = "Wiśniewska",
                    Email = "oliwia.wisniewska@example.com",
                    PhoneNumber = "987654330",
                    isPatient = false,
                    isDietician = true,
                    isAdmin = false,
                    BirthDate = new DateTime(1990, 10, 10),
                    AddressId = 10,
                    EmailConfirmed = true,
                    PublicId = "uq3skdkbnykebsuvewop",
                    PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273746/uq3skdkbnykebsuvewop.jpg",
                    //RatingId = 5
                };
                await userManager.CreateAsync(dietician3, "Pa$$w0rd5555555554!");
            }

            await context.SaveChangesAsync();

            #endregion
            #region Nutrient
            // Sprawdzanie i dodawanie testowych rekordów dla Nutrient
            if (!context.NutrientsDb.Any())
            {
                var nutrients = new List<Nutrient>()
            {
                new Nutrient { NutritionixId = 301, NamePL = "Wapń", NameEN = "Calcium, Ca", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 205, NamePL = "Węglowodany", NameEN = "Carbohydrate, by difference", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 601, NamePL = "Cholesterol", NameEN = "Cholesterol", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 208, NamePL = "Energia", NameEN = "Energy", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "kcal").Id },
                new Nutrient { NutritionixId = 606, NamePL = "Kwasy tłuszczowe nasycone", NameEN = "Fatty acids, total saturated", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 204, NamePL = "Tłuszcze", NameEN = "Total lipid (fat)", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 605, NamePL = "Kwasy tłuszczowe trans", NameEN = "Fatty acids, total trans", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 303, NamePL = "Żelazo", NameEN = "Iron, Fe", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 291, NamePL = "Błonnik", NameEN = "Fiber, total dietary", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 306, NamePL = "Potas", NameEN = "Potassium, K", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 307, NamePL = "Sód", NameEN = "Sodium, Na", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 203, NamePL = "Białko", NameEN = "Protein", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 269, NamePL = "Cukry", NameEN = "Sugars, total", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 539, NamePL = "Cukry dodane", NameEN = "Sugars, added", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 324, NamePL = "Witamina D", NameEN = "Vitamin D", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "IU").Id },
                new Nutrient { NutritionixId = 299, NamePL = "Alkohol cukrowy", NameEN = "Sugar Alcohol", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 1001, NamePL = "Erytrytol", NameEN = "Erythritol", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 1006, NamePL = "Aluloza", NameEN = "Allulose", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 1002, NamePL = "Gliceryna", NameEN = "Glycerin", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 290, NamePL = "Ksylitol", NameEN = "Xylitol", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 261, NamePL = "Sorbitol", NameEN = "Sorbitol",IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 260, NamePL = "Mannitol", NameEN = "Mannitol",IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 1003, NamePL = "Maltitol", NameEN = "Maltitol",IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 1004, NamePL = "Izomaltoza", NameEN = "Isomalt", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 1005, NamePL = "Laktytol", NameEN = "Lactitol", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 513, NamePL = "Alanina", NameEN = "Alanine", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 221, NamePL = "Alkohol etylowy", NameEN = "Alcohol, ethyl", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 511, NamePL = "Arginina", NameEN = "Arginine", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 207, NamePL = "Popiół", NameEN = "Ash", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 514, NamePL = "Kwas asparaginowy", NameEN = "Aspartic acid", IsMacronutrient = true, IsMicronutrient = false, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 454, NamePL = "Betaina", NameEN = "Betaine", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 262, NamePL = "Kofeina", NameEN = "Caffeine", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 639, NamePL = "Kampesterol", NameEN = "Campesterol", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 322, NamePL = "Karoten alfa", NameEN = "Carotene, alpha", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 321, NamePL = "Karoten beta", NameEN = "Carotene, beta", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 326, NamePL = "Witamina D3 (cholekalcyferol)", NameEN = "Vitamin D3 (cholecalciferol)", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 421, NamePL = "Cholina, ogółem", NameEN = "Choline, total", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 334, NamePL = "Kryptoksantyna beta", NameEN = "Cryptoxanthin, beta", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 312, NamePL = "Miedź, Cu", NameEN = "Copper, Cu", IsMacronutrient = false, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 507, NamePL = "Cysteina", NameEN = "Cystine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 268, NamePL = "Energia", NameEN = "Energy", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "kJ").Id },
                new Nutrient { NutritionixId = 325, NamePL = "Witamina D2 (ergokalcyferol)", NameEN = "Vitamin D2 (ergocalciferol)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 610, NamePL = "10:00", NameEN = "10:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 611, NamePL = "12:00", NameEN = "12:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 696, NamePL = "13:00", NameEN = "13:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 612, NamePL = "14:00", NameEN = "14:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 625, NamePL = "14:01", NameEN = "14:01", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 652, NamePL = "15:00", NameEN = "15:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 697, NamePL = "15:01", NameEN = "15:01", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 613, NamePL = "16:00", NameEN = "16:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 626, NamePL = "16:1 undifferentiated", NameEN = "16:1 undifferentiated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 673, NamePL = "16:1 c", NameEN = "16:1 c", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 662, NamePL = "16:1 t", NameEN = "16:1 t", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 653, NamePL = "17:00", NameEN = "17:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 687, NamePL = "17:01", NameEN = "17:01", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 614, NamePL = "18:00", NameEN = "18:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 617, NamePL = "18:1 undifferentiated", NameEN = "18:1 undifferentiated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 674, NamePL = "18:1 c", NameEN = "18:1 c", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 663, NamePL = "18:1 t", NameEN = "18:1 t", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 859, NamePL = "18:1-11t (18:1t n-7)", NameEN = "18:1-11t (18:1t n-7)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 618, NamePL = "18:2 undifferentiated", NameEN = "18:2 undifferentiated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 670, NamePL = "18:2 CLAs", NameEN = "18:2 CLAs", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 675, NamePL = "18:2 n-6 c,c", NameEN = "18:2 n-6 c,c", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 669, NamePL = "18:2 t,t", NameEN = "18:2 t,t", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 619, NamePL = "18:3 undifferentiated", NameEN = "18:3 undifferentiated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 851, NamePL = "18:3 n-3 c,c,c (ALA)", NameEN = "18:3 n-3 c,c,c (ALA)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 685, NamePL = "18:3 n-6 c,c,c", NameEN = "18:3 n-6 c,c,c", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 627, NamePL = "18:04", NameEN = "18:04", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 615, NamePL = "20:00", NameEN = "20:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 628, NamePL = "20:01", NameEN = "20:01", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 672, NamePL = "20:2 n-6 c,c", NameEN = "20:2 n-6 c,c", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 689, NamePL = "20:3 undifferentiated", NameEN = "20:3 undifferentiated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 852, NamePL = "20:3 n-3", NameEN = "20:3 n-3", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 853, NamePL = "20:3 n-6", NameEN = "20:3 n-6", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 620, NamePL = "20:4 undifferentiated", NameEN = "20:4 undifferentiated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 855, NamePL = "20:4 n-6", NameEN = "20:4 n-6", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 629, NamePL = "20:5 n-3 (EPA)", NameEN = "20:5 n-3 (EPA)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 857, NamePL = "21:05", NameEN = "21:05", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 624, NamePL = "22:00", NameEN = "22:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 630, NamePL = "22:1 undifferentiated", NameEN = "22:1 undifferentiated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 858, NamePL = "22:04", NameEN = "22:04", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 631, NamePL = "22:5 n-3 (DPA)", NameEN = "22:5 n-3 (DPA)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 621, NamePL = "22:6 n-3 (DHA)", NameEN = "22:6 n-3 (DHA)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 654, NamePL = "24:00:00", NameEN = "24:00:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 671, NamePL = "24:1 c", NameEN = "24:1 c", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 607, NamePL = "4:00", NameEN = "4:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 608, NamePL = "6:00", NameEN = "6:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 609, NamePL = "8:00", NameEN = "8:00", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 645, NamePL = "Kwasy tłuszczowe jednonienasycone", NameEN = "Fatty acids, total monounsaturated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 646, NamePL = "Kwasy tłuszczowe wielonienasycone", NameEN = "Fatty acids, total polyunsaturated", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 693, NamePL = "Kwasy tłuszczowe trans-monoenoic", NameEN = "Fatty acids, total trans-monoenoic", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 695, NamePL = "Kwasy tłuszczowe trans-polyenoic", NameEN = "Fatty acids, total trans-polyenoic", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 313, NamePL = "Fluor, F", NameEN = "Fluoride, F", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 417, NamePL = "Kwas foliowy, ogółem", NameEN = "Folate, total", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 431, NamePL = "Kwas foliowy", NameEN = "Folic acid", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 435, NamePL = "Kwas foliowy, DFE", NameEN = "Folate, DFE", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 432, NamePL = "Kwas foliowy, food", NameEN = "Folate, food", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 212, NamePL = "Fruktoza", NameEN = "Fructose", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 287, NamePL = "Galaktoza", NameEN = "Galactose", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 515, NamePL = "Kwas glutaminowy", NameEN = "Glutamic acid", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 211, NamePL = "Glukoza (dekstroza)", NameEN = "Glucose (dextrose)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 516, NamePL = "Glicyna", NameEN = "Glycine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 512, NamePL = "Histydyna", NameEN = "Histidine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 521, NamePL = "Hydroksyprolina", NameEN = "Hydroxyproline", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 503, NamePL = "Izoleucyna", NameEN = "Isoleucine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 213, NamePL = "Laktoza", NameEN = "Lactose", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 504, NamePL = "Leucyna", NameEN = "Leucine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 338, NamePL = "Luteina + zeaksantyna", NameEN = "Lutein + zeaxanthin", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 337, NamePL = "Lycopene", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 505, NamePL = "Lizyna", NameEN = "Lysine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 214, NamePL = "Maltoza", NameEN = "Maltose", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 506, NamePL = "Metionina", NameEN = "Methionine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 304, NamePL = "Magnez, Mg", NameEN = "Magnesium, Mg", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 428, NamePL = "Menachinon-4", NameEN = "Menaquinone-4", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 315, NamePL = "Mangan, Mn", NameEN = "Manganese, Mn", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 406, NamePL = "Niacyna", NameEN = "Niacin", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 573, NamePL = "Witamina E, dodana", NameEN = "Vitamin E, added", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 578, NamePL = "Witamina B-12, dodana", NameEN = "Vitamin B-12, added", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 257, NamePL = "Dostosowany białko", NameEN = "Adjusted Protein", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 664, NamePL = "22:1 t", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 676, NamePL = "22:1 c", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 856, NamePL = "18:3i", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 665, NamePL = "18:2 t not further defined", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 666, NamePL = "18:2 i", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 305, NamePL = "Fosfor, P", NameEN = "Phosphorus, P", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 410, NamePL = "Kwas pantotenowy", NameEN = "Pantothenic acid", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 508, NamePL = "Fenyloalanina", NameEN = "Phenylalanine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 636, NamePL = "Fitosterole", NameEN = "Phytosterols", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 517, NamePL = "Prolina", NameEN = "Proline", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 319, NamePL = "Retinol", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 405, NamePL = "Ryboflawina", NameEN = "Riboflavin", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 317, NamePL = "Selen, Se", NameEN = "Selenium, Se", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 518, NamePL = "Seryna", NameEN = "Serine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 641, NamePL = "Beta-sitosterol", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 209, NamePL = "Skrobia", NameEN = "Starch", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 638, NamePL = "Stigmasterol", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 210, NamePL = "Sukroza", NameEN = "Sucrose", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 263, NamePL = "Teobromina", NameEN = "Theobromine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 404, NamePL = "Tiamina", NameEN = "Thiamin", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 502, NamePL = "Treonia", NameEN = "Threonine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 323, NamePL = "Witamina E (alfa-tokoferol)", NameEN = "Vitamin E (alpha-tocopherol)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 341, NamePL = "Tokoferol, beta", NameEN = "Tocopherol, beta", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 343, NamePL = "Tokoferol, delta", NameEN = "Tocopherol, delta", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 342, NamePL = "Tokoferol, gamma", NameEN = "Tocopherol, gamma", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 501, NamePL = "Tryptofan", NameEN = "Tryptophan", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 509, NamePL = "Tyrozyna", NameEN = "Tyrosine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 510, NamePL = "Walina", NameEN = "Valine", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 318, NamePL = "Witamina A, IU", NameEN = "Vitamin A, IU", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "IU").Id },
                new Nutrient { NutritionixId = 320, NamePL = "Witamina A, RAE", NameEN = "Vitamin A, RAE", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 418, NamePL = "Witamina B-12", NameEN = "Vitamin B-12", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 415, NamePL = "Witamina B-6", NameEN = "Vitamin B-6", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 401, NamePL = "Witamina C, kwas askorbinowy", NameEN = "Vitamin C, total ascorbic acid", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 328, NamePL = "Witamina D (D2 + D3)", NameEN = "Vitamin D (D2 + D3)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 430, NamePL = "Witamina K (filochinon)", NameEN = "Vitamin K (phylloquinone)", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 429, NamePL = "Dihydrofilochinon", NameEN = "Dihydrophylloquinone", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "Âµg").Id },
                new Nutrient { NutritionixId = 255, NamePL = "Woda", NameEN = "Water", IsMacronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "g").Id },
                new Nutrient { NutritionixId = 309, NamePL = "Cynk, Zn", NameEN = "Zinc, Zn", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 344, NamePL = "Tokoferol, alfa", NameEN = "Tocotrienol, alpha", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 345, NamePL = "Tokoferol, beta", NameEN = "Tocotrienol, beta", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 346, NamePL = "Tokoferol, gamma", NameEN = "Tocotrienol, gamma", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id },
                new Nutrient { NutritionixId = 347, NamePL = "Tokoferol, delta", NameEN = "Tocotrienol, delta", IsMacronutrient = true, IsMicronutrient = true, UnitId = context.UnitsDb.Single(u => u.Symbol == "mg").Id }
            };
                await context.NutrientsDb.AddRangeAsync(nutrients);
            }
            #endregion

            await context.SaveChangesAsync();
        }

        // ******************** DANE Z KLUCZAMI KLUCZY OBCYCH **************************************** //
        private static async Task SeedSecondClassesWithForeignKey(DietContext context)
        {
            #region UserRoles
            if (!context.UserRoles.Any())
            {
                var userRoles = new List<IdentityUserRole<int>>()
                {
                    new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                    new IdentityUserRole<int> { UserId = 1, RoleId = 2 },
                    new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                    new IdentityUserRole<int> { UserId = 3, RoleId = 2 },
                    new IdentityUserRole<int> { UserId = 4, RoleId = 3 },
                    new IdentityUserRole<int> { UserId = 5, RoleId = 3 },
                    new IdentityUserRole<int> { UserId = 6, RoleId = 3 },
                    new IdentityUserRole<int> { UserId = 7, RoleId = 4 },
                    new IdentityUserRole<int> { UserId = 8, RoleId = 4 },
                    new IdentityUserRole<int> { UserId = 9, RoleId = 4 }
                };
                await context.UserRoles.AddRangeAsync(userRoles);
            }
            #endregion
            #region Dishes
            // Sprawdzanie i dodawanie testowych danych dla Dish
            List<Dish> dishes;

            if (!context.DishesDb.Any())
            {
                dishes = new List<Dish>()
                {
                    new Dish { Name = "Makaron Bolognese", NameEN="Pasta Bolognese", Calories = 123, ServingQuantity = 1, MeasureId = 1, UnitId = 4, GlycemicIndex = 98, PreparingTime = "3:20", isActive = true, dateAdded = DateTime.Now, dateUpdated = null, dateDeleted = null, whoAdded = "Admin", whoUpdated = null, whoDeleted = null },
                    new Dish { Name = "salatka z grillow kurcz", NameEN="Grilled Chicken Salad",  Calories = 250, ServingQuantity = 1, MeasureId = 3, UnitId = 7, GlycemicIndex = 69, PreparingTime = "1:15", isActive = true, dateAdded = DateTime.Now, dateUpdated = null, dateDeleted = null, whoAdded = "Admin", whoUpdated = null, whoDeleted = null },
                    new Dish { Name = "Margherita Pizza", NameEN="Margherita Pizza", Calories = 698, ServingQuantity = 1, MeasureId = 2, UnitId = 2, GlycemicIndex = 22, PreparingTime = "0:50", isActive = true, dateAdded = DateTime.Now, dateUpdated = null, dateDeleted = null, whoAdded = "Admin", whoUpdated = null, whoDeleted = null },
                };
                await context.DishesDb.AddRangeAsync(dishes);
                context.SaveChanges();

                #region Recipes
                if (!context.RecipesDb.Any())
                {
                    var recipes = new List<Recipe>()
                    {
                        new Recipe { DishId = dishes[0].Id, isActive = true, dateAdded = DateTime.Now },
                        new Recipe { DishId = dishes[1].Id, isActive = true, dateAdded = DateTime.Now },
                        new Recipe { DishId = dishes[2].Id, isActive = true, dateAdded = DateTime.Now }
                    };
                    await context.RecipesDb.AddRangeAsync(recipes);
                    context.SaveChanges();

                    dishes[0].Recipe = recipes[0];
                    dishes[1].Recipe = recipes[1];
                    dishes[2].Recipe = recipes[2];

                    context.SaveChanges();
                }
                #endregion
            }
            #endregion
            #region FoodCatalogs
            if (!context.FoodCatalogsDb.Any())
            {
                var foodCatalogs = new List<FoodCatalog>()
                {
                    new FoodCatalog { CatalogName = "Ogolny" },
                    new FoodCatalog { CatalogName = "Bezmleczne" },
                    new FoodCatalog { CatalogName = "Dla cukrzyków" },
                    new FoodCatalog { CatalogName = "Keto" },
                    new FoodCatalog { CatalogName = "Hashimoto" }
                };
                await context.FoodCatalogsDb.AddRangeAsync(foodCatalogs);
            }
            #endregion
            #region MessagesToDietician
            //Sprawdzanie i dodawanie testowych wiadomości do dietetyka
            if (!context.MessageToDb.Any())
            {
                var messages = new List<MessageTo>()
                {
                    new MessageTo { Title = "Pytanie o dietę 1", Description = "Mam pytanie odnośnie ilości węglowodanów w diecie.", DieticianId = 7, PatientId = 4, IsRead = false, ReadDate = null, isActive = true, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Kwestia alergii", Description = "Czy produkt X jest odpowiedni dla osoby z alergią na orzechy?", DieticianId = 8, PatientId = 4, IsRead = false, ReadDate = null, isActive = true, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Porada dla sportowca", Description = "Jakie produkty zalecasz dla aktywnych fizycznie osób?", DieticianId = 8, PatientId = 6, IsRead = false, ReadDate = null, isActive = true, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Suplementacja", Description = "Czy warto suplementować witaminę D w okresie zimowym?", DieticianId = 7, PatientId = 5, IsRead = false, ReadDate = null, isActive = true, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Pytanie o jadłospis", Description = "Czy możesz mi pomóc skomponować jadłospis na nadchodzący tydzień?", DieticianId = 8, PatientId = 5, IsRead = false, ReadDate = null, isActive = true, dateAdded = DateTime.Now },
                };
                await context.MessageToDb.AddRangeAsync(messages);
            }
            #endregion
            #region MessageToPatient
            // Sprawdzanie i dodawanie testowych wiadomości do pacjentów
            if (!context.MessageToDb.Any())
            {
                var messagesToPatients = new List<MessageTo>()
                {
                    new MessageTo { Title = "Konsultacja dietetyczna", Description = "Witaj! Zapraszam na konsultację dietetyczną w przyszłym tygodniu. Daj mi znać, kiedy Ci pasuje.", PatientId = 4, AdminId = 2, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Zmiana planu dietetycznego", Description = "Witam! Zaktualizowałem Twój plan dietetyczny. Sprawdź go w aplikacji i daj mi znać, czy wszystko jest jasne.", PatientId = 4, AdminId = 3, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Przypomnienie o wizycie", Description = "Przypominam o jutrzejszej wizycie. Jeśli nie możesz przyjść, daj mi znać jak najszybciej.", PatientId = 5, AdminId = 2, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Wyniki badań", Description = "Twoje wyniki badań są już dostępne. Zalecam omówienie ich podczas najbliższej wizyty.", PatientId = 5, AdminId = 3, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Zalecenia po wizycie", Description = "Witaj! Po naszej ostatniej wizycie przygotowałem kilka zaleceń. Sprawdź je w aplikacji i postępuj zgodnie z nimi.", PatientId = 6, AdminId = 3, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                };
                await context.MessageToDb.AddRangeAsync(messagesToPatients);
            }
            #endregion
            #region MessagesToAdmin
            if (!context.MessageToDb.Any())
            {
                var messagesToAdmins = new List<MessageTo>()
                {
                    new MessageTo { Title = "Zgłoszenie problemu technicznego", Description = "Witaj! Napotkaliśmy problem z funkcją wysyłania wiadomości w aplikacji. Prosimy o szybką interwencję.", AdminId = 2, DieticianId = 8, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Aktualizacja polityki prywatności", Description = "Witaj! Przesyłam aktualizację polityki prywatności. Proszę o jej przejrzenie i zatwierdzenie.", AdminId = 3, DieticianId = 9, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                    new MessageTo { Title = "Propozycja współpracy", Description = "Dzień dobry! Mamy propozycję współpracy, która może być interesująca dla naszej platformy. Proszę o kontakt.", AdminId = 2, DieticianId = 7, isActive = true, IsRead = false, ReadDate = null, dateAdded = DateTime.Now },
                };
                await context.MessageToDb.AddRangeAsync(messagesToAdmins);
            }
            #endregion
            #region PatientCards
            if (!context.PatientCardsDb.Any())
            {
                var patientCards = new List<PatientCard>
                {
                    new PatientCard { PatientId = 4, SexId = 1, DieticianId = 7 },
                    new PatientCard { PatientId = 5, SexId = 1, DieticianId = 8 },
                    new PatientCard { PatientId = 6, SexId = 2, DieticianId = 9 },
                    new PatientCard { PatientId = 4, SexId = 1, DieticianId = 8 },
                    new PatientCard { PatientId = 4, SexId = 1, DieticianId = 9 }
                };

                await context.PatientCardsDb.AddRangeAsync(patientCards);
                context.SaveChanges();

                var surveys = new List<Survey>
                {
                    new Survey { Heigth = 170, Weith = 70, MeasureTime = DateTime.Now.AddDays(-30), DieticianId = 7 },
                    new Survey { Heigth = 165, Weith = 65, MeasureTime = DateTime.Now.AddDays(-20), DieticianId = 8 },
                    new Survey { Heigth = 180, Weith = 80, MeasureTime = DateTime.Now.AddDays(-10), DieticianId = 9 },
                    new Survey { Heigth = 170, Weith = 65, MeasureTime = DateTime.Now.AddDays(-20), DieticianId = 7 },
                    new Survey { Heigth = 170, Weith = 62, MeasureTime = DateTime.Now.AddDays(-10), DieticianId = 7 }
                };

                await context.SurveysDb.AddRangeAsync(surveys);
                context.SaveChanges();

                var patientCardSurveys = new List<PatientCardSurvey>
                {
                    new PatientCardSurvey { PatientCardId = 1, SurveyId = 1, DieticianId = 7},
                    new PatientCardSurvey { PatientCardId = 2, SurveyId = 2, DieticianId = 7},
                    new PatientCardSurvey { PatientCardId = 3, SurveyId = 3, DieticianId = 7},
                    new PatientCardSurvey { PatientCardId = 4, SurveyId = 4, DieticianId = 8},
                    new PatientCardSurvey { PatientCardId = 5, SurveyId = 5, DieticianId = 8}
                };

                await context.PatientCardSurveysDb.AddRangeAsync(patientCardSurveys);
                context.SaveChanges();
            }
            #endregion
            #region Diets
            if (!context.DietsDb.Any())
            {
                var diets = new List<Diet>()
                {
                    new Diet { Name = "Dieta1", StartDate = new DateTime(2023, 12, 13), EndDate = new DateTime(2023, 12, 14), PatientId = 4, numberOfMeals = 4, DieteticianId = 7, isActive = true, dateAdded = DateTime.Now },
                    new Diet { Name = "Dieta2", StartDate = new DateTime(2023, 12, 10), EndDate = new DateTime(2023, 12, 11), PatientId = 5, numberOfMeals = 2, DieteticianId = 7, isActive = true, dateAdded = DateTime.Now },
                    new Diet { Name = "Dieta3", StartDate = new DateTime(2023, 12, 14), EndDate = new DateTime(2023, 12, 24), PatientId = 5, numberOfMeals = 5, DieteticianId = 7, isActive = true, dateAdded = DateTime.Now },
                    new Diet { Name = "Dieta4", StartDate = new DateTime(2023, 12, 15), EndDate = new DateTime(2023, 12, 31), PatientId = 6, numberOfMeals = 4, DieteticianId = 8, isActive = true, dateAdded = DateTime.Now },
                };
                await context.DietsDb.AddRangeAsync(diets);
            }
            #endregion
            #region Diplomas
            // Dodawanie testowych danych dla Diploma
            if (!context.DiplomasDb.Any())
            {
                var diplomas = new List<Diploma>()
                    {
                        new Diploma {
                            Title = "Certyfikat Specjalisty ds. Dietetyki Klinicznej",
                            Description = "Specjalizacja w planowaniu diet i programów żywieniowych dla pacjentów szpitalnych, z uwzględnieniem różnych stanów zdrowia i chorób.",
                            DieticianId = 7,
                            PublicId = "purg0amnvsmjptath0hh",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704272720/purg0amnvsmjptath0hh.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikowany Doradca Żywieniowy dla Sportowców",
                            Description = "Ekspertyza w tworzeniu planów żywieniowych dla sportowców, skupiająca się na optymalizacji wydajności i regeneracji po wysiłku.",
                            DieticianId = 7,
                            PublicId = "qmxrzg4siseewwslninx",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704272763/qmxrzg4siseewwslninx.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikowany Ekspert Dietetyki Dziecięcej",
                            Description = "Specjalizacja w żywieniu dzieci i młodzieży, z uwzględnieniem ich specyficznych potrzeb rozwojowych i dietetycznych.",
                            DieticianId = 7,
                            PublicId = "uqfz3cwaoabbnxljdrpv",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704272802/uqfz3cwaoabbnxljdrpv.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikat Specjalisty ds. Dietoterapii",
                            Description = "Skupienie na wykorzystaniu diety jako części terapii w leczeniu i zapobieganiu różnym schorzeniom.",
                            DieticianId = 7,
                            PublicId = "uxlfrbyec2aqehfhhxcd",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704272842/uxlfrbyec2aqehfhhxcd.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikowany Dietetyk Integracyjny",
                            Description = "Połączenie wiedzy z zakresu dietetyki z podejściem holistycznym do zdrowia, uwzględniającym zarówno ciało, jak i umysł.",
                            DieticianId = 8,
                            PublicId = "bkqh1fihqj0mghpweecx",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273421/bkqh1fihqj0mghpweecx.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikowany Ekspert Żywienia Wegetariańskiego i Wegańskiego",
                            Description = "Specjalizacja w tworzeniu zbilansowanych, pełnowartościowych diet bez produktów pochodzenia zwierzęcego.",
                            DieticianId = 8,
                            PublicId = "gnmzkvyh9bt1aypbheon",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273454/gnmzkvyh9bt1aypbheon.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikat Specjalisty ds. Żywienia w Chorobach Metabolicznych",
                            Description = "Ekspertyza w planowaniu diet i programów żywieniowych dla osób cierpiących na choroby metaboliczne, jak cukrzyca czy otyłość.",
                            DieticianId = 8,
                            PublicId = "nmuqu11gdm2saqst44h1",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273490/nmuqu11gdm2saqst44h1.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikat Eksperta ds. Żywienia i Suplementacji",
                            Description = "Skupienie na odpowiednim doborze suplementów diety i wzbogacaniu diety w niezbędne składniki odżywcze.",
                            DieticianId = 8,
                            PublicId = "cbrywy2xzomp5ukbn7jv",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273527/cbrywy2xzomp5ukbn7jv.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikowany Doradca Żywieniowy w Geriatrii",
                            Description = "Specjalizacja w żywieniu osób starszych, z uwzględnieniem ich specyficznych potrzeb zdrowotnych i odżywczych.",
                            DieticianId = 9,
                            PublicId = "wsttzfpmlxxwrkyubegt",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273806/wsttzfpmlxxwrkyubegt.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikowany Specjalista Żywienia w Zaburzeniach Odżywiania",
                            Description = "Ekspertyza w pracy z osobami cierpiącymi na zaburzenia odżywiania, jak anoreksja czy bulimia.",
                            DieticianId = 9,
                            PublicId = "robvaza6sdrjz8a4zlyz",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273837/robvaza6sdrjz8a4zlyz.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikat Specjalisty ds. Żywienia w Alergiach Pokarmowych",
                            Description = "Specjalizacja w tworzeniu diet eliminacyjnych i adaptacyjnych dla osób z alergiami pokarmowymi.",
                            DieticianId = 9,
                            PublicId = "oacmi5ncwo7kyisb5bee",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273865/oacmi5ncwo7kyisb5bee.jpg"
                        },
                        new Diploma {
                            Title = "Certyfikowany Konsultant Żywienia Holistycznego",
                            Description = "Skupienie na łączeniu wiedzy z różnych dziedzin zdrowia i żywienia w celu osiągnięcia ogólnego dobrostanu.",
                            DieticianId = 9,
                            PublicId = "iowl5ggpiuvvuhtcuelo",
                            PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273894/iowl5ggpiuvvuhtcuelo.jpg"
                        },
                    };
                await context.DiplomasDb.AddRangeAsync(diplomas);
                context.SaveChanges();
            }
            #endregion
            #region Logos
            if (!context.LogosDb.Any())
            {
                var logos = new List<Logo>()
                    {
                        new Logo { DieticianId = 7, PublicId = "tepj7s1sioxelh6tuilv", PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704272864/tepj7s1sioxelh6tuilv.jpg" },
                        new Logo { DieticianId = 8, PublicId = "wedqtab5lvwpc9odxaws", PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273542/wedqtab5lvwpc9odxaws.jpg" },
                        new Logo { DieticianId = 9, PublicId = "wnkixwfsfn4o6ypfg8yx", PictureUrl = "https://res.cloudinary.com/dqz9wmlcd/image/upload/v1704273911/wnkixwfsfn4o6ypfg8yx.jpg" },
                    };
                await context.LogosDb.AddRangeAsync(logos);
            }
            #endregion
            #region Offices
            // Dodawanie testowych danych dla Office
            if (!context.OfficesDb.Any())
            {
                var offices = new List<Office>()
                    {
                        new Office { OfficeName = "DietMax", AddressId = 11 },
                        new Office { OfficeName = "CreativeMind", AddressId = 12 },
                        new Office { OfficeName = "DietPlan", AddressId = 13 },
                        new Office { OfficeName = "DietTrotter", AddressId = 14 },
                        new Office { OfficeName = "Dr Slump", AddressId = 15 },
                    };
                await context.OfficesDb.AddRangeAsync(offices);
                context.SaveChanges();
            }
            #endregion
            #region DieticianOffices
            if (!context.DieticianOffices.Any())
            {
                var dieticianOffices = new List<DieticianOffice>()
                    {
                        new DieticianOffice { DieticianId = 7, OfficeId =1 },
                        new DieticianOffice { DieticianId = 7, OfficeId =2 },
                        new DieticianOffice { DieticianId = 8, OfficeId =3 },
                        new DieticianOffice { DieticianId = 9, OfficeId =4 },
                        new DieticianOffice { DieticianId = 9, OfficeId =5 },
                    };
                await context.DieticianOffices.AddRangeAsync(dieticianOffices);
                context.SaveChanges();
            }
            #endregion
            #region DieticianSpecializations
            // Dodawanie testowych danych dla DieticianSpecialization
            if (!context.DieticianSpecialization.Any())
            {
                var dieticianSpecializations = new List<DieticianSpecialization>()
                    {
                        new DieticianSpecialization { DieticianId =7 , SpecializationId =7  },
                        new DieticianSpecialization { DieticianId =7 , SpecializationId =4  },
                        new DieticianSpecialization { DieticianId =7 , SpecializationId =15  },
                        new DieticianSpecialization { DieticianId =8 , SpecializationId =18  },
                        new DieticianSpecialization { DieticianId =8 , SpecializationId =9  },
                        new DieticianSpecialization { DieticianId =8 , SpecializationId =4  },
                        new DieticianSpecialization { DieticianId =8 , SpecializationId =12  },
                        new DieticianSpecialization { DieticianId =8 , SpecializationId =8  },
                        new DieticianSpecialization { DieticianId =9 , SpecializationId =2  },
                        new DieticianSpecialization { DieticianId =9 , SpecializationId =1  },
                        new DieticianSpecialization { DieticianId =9 , SpecializationId =19  },
                    };
                await context.DieticianSpecialization.AddRangeAsync(dieticianSpecializations);
                context.SaveChanges();
            }
            #endregion
            #region Ingredients
            if (!context.IngredientsDb.Any())
            {
                var ingredients = new List<Ingredient>()
                {
                        new Ingredient { Name = "Ser Biały", Calories = 100, ServingQuantity = 1, MeasureId = 1, Weight = 28, UnitId = 1, DieticianId = 7, GlycemicIndex = 30 },
                        new Ingredient { Name = "Jogurt Naturalny", Calories = 50, ServingQuantity = 1, MeasureId = 2, Weight = 200, UnitId = 1, DieticianId = 8, GlycemicIndex = 40 },
                        new Ingredient { Name = "Oliwa z Oliwek", Calories = 120, ServingQuantity = 1, MeasureId = 3, Weight = 15, UnitId = 1, DieticianId = 9, GlycemicIndex = 10 },
                        new Ingredient { Name = "Pierś Kurczaka", Calories = 150, ServingQuantity = 1, MeasureId = 4, Weight = 100, UnitId = 2, DieticianId = 7, GlycemicIndex = 20 },
                        new Ingredient { Name = "Mleko 2%", NameEN = "2% milk", Calories = 122f, MeasureId = 1, Weight = 244, UnitId = 1, GlycemicIndex = null, DieticianId = null },
                        new Ingredient { Name = "Pomidor", Calories = 20, ServingQuantity = 1, MeasureId = 1, Weight = 150, UnitId = 3, DieticianId = 8, GlycemicIndex = 15 },
                        new Ingredient { Name = "Owsianka", Calories = 120, ServingQuantity = 1, MeasureId = 2, Weight = 40, UnitId = 1, DieticianId = 9, GlycemicIndex = 50 },
                        new Ingredient { Name = "Ziemniaki", NameEN = "potato", Calories = 160.89f, MeasureId = 1, Weight = 21, UnitId = 5, GlycemicIndex = null, DieticianId = null },
                        new Ingredient { Name = "Ogórek", NameEN = "cucumber", Calories = 30.15f, MeasureId = 1, Weight = 10, UnitId = 7, GlycemicIndex = null, DieticianId = null },
                        new Ingredient { Name = "Banany", Calories = 90, ServingQuantity = 1, MeasureId = 3, Weight = 120, UnitId = 1, DieticianId = 7, GlycemicIndex = 60 },
                        new Ingredient { Name = "Orzechy Włoskie", Calories = 200, ServingQuantity = 1, MeasureId = 4, Weight = 30, UnitId = 1, DieticianId = 8, GlycemicIndex = 25 },
                        new Ingredient { Name = "Brokuły", Calories = 30, ServingQuantity = 1, MeasureId = 1, Weight = 150, UnitId = 1, DieticianId = 9, GlycemicIndex = 15 },
                        new Ingredient { Name = "Chleb Pełnoziarnisty", Calories = 80, ServingQuantity = 1, MeasureId = 2, Weight = 40, UnitId = 1, DieticianId = 7, GlycemicIndex = 35 },
                        new Ingredient { Name = "Ser", NameEN = "cheese", Calories = 113.12f, MeasureId = 1, Weight = 28, UnitId = 1, GlycemicIndex = null, DieticianId = null },
                        new Ingredient { Name = "Kapusta surowa", NameEN = "cabbage, raw", Calories = 22.25f, MeasureId = 1, Weight = 17, UnitId = 4, GlycemicIndex = null, DieticianId = null },
                        new Ingredient { Name = "Bardzo duże jajko", NameEN = "extra large egg", Calories = 90.09f, MeasureId = 1, Weight = 34, UnitId = 1, GlycemicIndex = null, DieticianId = null },
                };
                await context.IngredientsDb.AddRangeAsync(ingredients);
            }
            #endregion

            await context.SaveChangesAsync();
        }

        private static async Task SeedThirdClassesWithForeignKey(DietContext context)
        {
            #region RecipeSteps
            if (!context.RecipeStepsDb.Any())
            {
                var recipeSteps = new List<RecipeStep>()
                {
                    new RecipeStep { StepNumber = 1, Description = "krok1", RecipeId = 1, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 2, Description = "krok2", RecipeId = 1, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 3, Description = "krok3", RecipeId = 1, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 4, Description = "krok4", RecipeId = 1, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 1, Description = "krok1 dla przepisu 2", RecipeId = 2, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 2, Description = "krok2 dla przepisu 2", RecipeId = 2, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 1, Description = "wykonanie przep 3", RecipeId = 3, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 2, Description = "kolejny z przepis 3", RecipeId = 3, isActive = true, dateAdded = DateTime.Now },
                    new RecipeStep { StepNumber = 3, Description = "i ostatni z przepi 3", RecipeId = 3, isActive = true, dateAdded = DateTime.Now },
                };
                await context.RecipeStepsDb.AddRangeAsync(recipeSteps);
            }
            #endregion
            #region MealTimesToXYAxis
            // Sprawdzanie i dodawanie testowych danych dla MealTimes
            if (!context.MealTimesDb.Any())
            {
                var mealTimes = new List<MealTimeToXYAxis>()
                {
                    new MealTimeToXYAxis { MealId = 1, MealTime = new DateTime(2023, 12, 13), DietId = 2, DishId = 1 },
                    new MealTimeToXYAxis { MealId = 3, MealTime = new DateTime(2023, 12, 13), DietId = 2, DishId = 2  },
                    new MealTimeToXYAxis { MealId = 5, MealTime = new DateTime(2023, 12, 13), DietId = 2, DishId = 3  },
                    new MealTimeToXYAxis { MealId = 1, MealTime = new DateTime(2023, 12, 14), DietId = 2, DishId = 2  },
                    new MealTimeToXYAxis { MealId = 3, MealTime = new DateTime(2023, 12, 14), DietId = 2, DishId = 3  },
                    new MealTimeToXYAxis { MealId = 5, MealTime = new DateTime(2023, 12, 14), DietId = 2, DishId = 1  }
                };
                await context.MealTimesDb.AddRangeAsync(mealTimes);
            }
            #endregion
            //#region DieticianPatients
            //if (!context.DieticianPatientsDb.Any())
            //{
            //    var dieticianPatients = new List<DieticianPatient>
            //    {
            //        new DieticianPatient { PatientId = 4, DieticianId = 7 },
            //        new DieticianPatient { PatientId = 5, DieticianId = 8 },
            //        new DieticianPatient { PatientId = 6, DieticianId = 9 },
            //        new DieticianPatient { PatientId = 4, DieticianId = 7 },
            //        new DieticianPatient { PatientId = 4, DieticianId = 9 }
            //    };
            //    await context.DieticianPatientsDb.AddRangeAsync(dieticianPatients);
            //};
            //#endregion

            await context.SaveChangesAsync();
        }

        private static async Task ExecuteSqlCommands(DietContext context)
        {
            #region ApplySQLCommandsForSpTriggView

            await context.Database.ExecuteSqlRawAsync("USE [DietDB];");
            // Polecenie CREATE VIEW
            await context.Database.ExecuteSqlRawAsync("CREATE OR ALTER VIEW [dbo].[GetAllSexesFromSqlView] AS " +
                "SELECT dbo.Sex.*, Id AS Expr1, Name AS Expr2 FROM dbo.Sex;");

            // Polecenie CREATE TRIGGER
            await context.Database.ExecuteSqlRawAsync(
                @"
                -- Jeśli trigger jest aktywowany przez operację INSERT
                CREATE OR ALTER TRIGGER [dbo].[CreateAddressForUser] 
                ON [dbo].[Users] AFTER INSERT 
                AS 
                BEGIN 
                    SET NOCOUNT ON; 

                    IF (SELECT COUNT(*) FROM inserted) > 0 
                    BEGIN 
                        DECLARE @UserId INT; 
                        DECLARE @AddressId INT; 
                        DECLARE @FirstName NVARCHAR(MAX); 
                        DECLARE @LastName NVARCHAR(MAX); 
                        DECLARE @BirthDate DATETIME; 
                        DECLARE @isPatient BIT; 
                        DECLARE @isDietician BIT; 
                        DECLARE @isAdmin BIT; 

                        SELECT TOP 1 
                            @UserId = Id, 
                            @AddressId = AddressId, 
                            @FirstName = FirstName, 
                            @LastName = LastName, 
                            @BirthDate = BirthDate, 
                            @isPatient = isPatient, 
                            @isDietician = isDietician, 
                            @isAdmin = isAdmin 
                        FROM inserted; 

                        IF (@isDietician = 1 OR @isPatient = 1 OR @isAdmin = 1) 
                        BEGIN 
                            INSERT INTO dbo.Address (City, CountryStateId, ZipCode, Country, Street, LocalNo, isActive, dateAdded) 

                            VALUES ('Uzupelnij dane', 1, 'Uzupelnij dane', 'Uzupelnij dane', 'Uzupelnij dane', 'Uzupelnij dane', 1, CURRENT_TIMESTAMP); 
                            
                            SET @AddressId = SCOPE_IDENTITY(); 

                            UPDATE dbo.Users 
                            SET AddressId = @AddressId 
                            WHERE Id = @UserId; 

                            IF @isDietician = 1 
                            BEGIN 
                                INSERT INTO dbo.UserRoles (UserId, RoleId) 
                                VALUES (@UserId, 4); 
                            END 
                            ELSE IF @isAdmin = 1 
                            BEGIN 
                                INSERT INTO dbo.UserRoles (UserId, RoleId) 
                                VALUES (@UserId, 2); 
                            END 
                            ELSE IF @isPatient = 1 
                            BEGIN 
                                INSERT INTO dbo.UserRoles (UserId, RoleId) 
                                VALUES (@UserId, 3); 
                            END 
                        END 
                    END 
                END;
                "
            );

            // Polecenie CREATE PROCEDURE
            await context.Database.ExecuteSqlRawAsync(
                @"
                CREATE OR ALTER PROCEDURE [dbo].[CreatePatientCard]
                    @PatientId INT,
                    @DieticianId INT,
                    @SexId INT
                AS
                BEGIN
                    INSERT INTO PatientCard (PatientId, DieticianId, SexId, isActive, dateAdded)
                    VALUES (@PatientId, @DieticianId, @SexId, 1, CURRENT_TIMESTAMP);

                    -- pobranie identyfikatora nowo dodanego wpisu (opcjonalne)
                    SELECT SCOPE_IDENTITY() AS NewPatientCardId;
                END;
                "
                );


            await context.Database.ExecuteSqlRawAsync(
                @"
                CREATE OR ALTER PROCEDURE [dbo].[GetPatientCard] 
                    @PatientId INT 
                AS
                BEGIN
                SELECT
                pc.Id AS PatientCardId,
                p.FirstName AS PatientFirstName,
                p.LastName AS PatientLastName,
                p.PictureUrl AS PatientPictureUrl,
                s.Name AS PatientSex,
                pcs.SurveyId,
                s2.Heigth AS Heigth,
                s2.Weith AS Weith,
                s2.MeasureTime AS MeasureTime,
                tr.Id AS TestResultId,
                str.test1 AS Test1,
                str.test2 AS Test2,
                str.test3 AS Test3,
                str.TestDate AS SingleTestResultDate
                FROM
                PatientCard pc
                JOIN
                Users p ON pc.PatientId = p.Id
                JOIN
                Sex s ON pc.SexId = s.Id
                LEFT JOIN
                PatientCardSurveysDb pcs ON pc.Id = pcs.PatientCardId
                LEFT JOIN
                Survey s2 ON pcs.SurveyId = s2.Id
                LEFT JOIN
                TestResult tr ON pc.Id = tr.PatientCardId
                LEFT JOIN
                SingleTestResults str ON tr.SingleTestResultsId = str.Id 
                WHERE p.Id = @PatientId;
                END;"
                );

            #endregion 
        }
    }
}