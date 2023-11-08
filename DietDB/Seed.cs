using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelsDB;
using ModelsDB.Functionality;
using Microsoft.EntityFrameworkCore;

namespace DietDB
{
    public class Seed
    {
        public static async Task SeedData(DietContext context)
        {
            #region Address
            // Sprawdzanie i dodawanie testowych rekordów dla adresów
            if (!context.AddressesDb.Any())
            {
                var addresses = new List<Address>()
    {
        new Address
        {
            City = "Warszawa",
            State = "Mazowieckie",
            ZipCode = "00-001",
            Country = "Polska",
            Street = "Marszałkowska",
            LocalNo = "24A"
        },
        new Address
        {
            City = "Kraków",
            State = "Małopolskie",
            ZipCode = "30-002",
            Country = "Polska",
            Street = "Floriańska",
            LocalNo = "15B"
        },
        new Address
        {
            City = "Wrocław",
            State = "Dolnośląskie",
            ZipCode = "50-002",
            Country = "Polska",
            Street = "Rynek",
            LocalNo = "12C"
        },
        new Address
        {
            City = "Poznań",
            State = "Wielkopolskie",
            ZipCode = "60-002",
            Country = "Polska",
            Street = "Stawna",
            LocalNo = "8D"
        },
        new Address
        {
            City = "Gdańsk",
            State = "Pomorskie",
            ZipCode = "80-002",
            Country = "Polska",
            Street = "Długa",
            LocalNo = "5E"
        },
        new Address
        {
            City = "Lublin",
            State = "Lubelskie",
            ZipCode = "20-001",
            Country = "Polska",
            Street = "Lipowa",
            LocalNo = "3F"
        },
        new Address
        {
            City = "Katowice",
            State = "Śląskie",
            ZipCode = "40-001",
            Country = "Polska",
            Street = "Mariacka",
            LocalNo = "2G"
        },
        new Address
        {
            City = "Bydgoszcz",
            State = "Kujawsko-Pomorskie",
            ZipCode = "85-001",
            Country = "Polska",
            Street = "Długa",
            LocalNo = "6H"
        },
        new Address
        {
            City = "Białystok",
            State = "Podlaskie",
            ZipCode = "15-001",
            Country = "Polska",
            Street = "Lipowa",
            LocalNo = "9I"
        },
        new Address
        {
            City = "Rzeszów",
            State = "Podkarpackie",
            ZipCode = "35-001",
            Country = "Polska",
            Street = "3 Maja",
            LocalNo = "4J"
        },
    };

                await context.AddressesDb.AddRangeAsync(addresses);
            }
            #endregion
            #region CategoryOfDiet
            // Sprawdzanie i dodawanie testowych kategorii diet
            if (!context.CategoryOfDietsDb.Any())
            {
                var categoriesOfDiets = new List<CategoryOfDiet>()
    {
        new CategoryOfDiet
        {
            CategoryName = "Wegetariańska"
        },
        new CategoryOfDiet
        {
            CategoryName = "Wegańska"
        },
        new CategoryOfDiet
        {
            CategoryName = "Keto"
        },
        new CategoryOfDiet
        {
            CategoryName = "Paleo"
        },
        new CategoryOfDiet
        {
            CategoryName = "Śródziemnomorska"
        },
        new CategoryOfDiet
        {
            CategoryName = "Niska zawartość węglowodanów"
        },
        new CategoryOfDiet
        {
            CategoryName = "Wysokobiałkowa"
        },
        new CategoryOfDiet
        {
            CategoryName = "Bezglutenowa"
        },
        new CategoryOfDiet
        {
            CategoryName = "Bezlaktozowa"
        },
        new CategoryOfDiet
        {
            CategoryName = "Surowa"
        }
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
            #region MealTimes
            // Sprawdzanie i dodawanie testowych danych dla MealTimes
            if (!context.MealTimesDb.Any())
            {
                var mealTimes = new List<MealTime>()
                {
                    new MealTime { DishTime = "Śniadanie" },
                    new MealTime { DishTime = "Drugie śniadanie" },
                    new MealTime { DishTime = "Obiad" },
                    new MealTime { DishTime = "Podwieczorek" },
                    new MealTime { DishTime = "Kolacja" },
                };
                await context.MealTimesDb.AddRangeAsync(mealTimes);
            }
            #endregion
            #region Measure
            // Sprawdzanie i dodawanie testowych jednostek miary
            if (!context.MeasuresDb.Any())
            {
                var measures = new List<Measure>()
    {
        new Measure
        {
            Name = "Kilogram"
        },
        new Measure
        {
            Name = "Gram"
        },
        new Measure
        {
            Name = "Litr"
        },
        new Measure
        {
            Name = "Mililitr"
        },
        new Measure
        {
            Name = "Sztuka"
        },
        new Measure
        {
            Name = "Łyżka"
        },
        new Measure
        {
            Name = "Łyżeczka"
        },
        new Measure
        {
            Name = "Szklanka"
        },
        new Measure
        {
            Name = "Opakowanie"
        },
        new Measure
        {
            Name = "Kostka"
        }
    };

                await context.MeasuresDb.AddRangeAsync(measures);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Rating
            // Sprawdzanie i dodawanie testowych ocen
            if (!context.RatingsDb.Any())
            {
                var ratings = new List<Rating>()
    {
        new Rating
        {
            Note = 5  // 5/5 - doskonała ocena
        },
        new Rating
        {
            Note = 4  // 4/5 - bardzo dobra ocena
        },
        new Rating
        {
            Note = 3  // 3/5 - średnia ocena
        },
        new Rating
        {
            Note = 2  // 2/5 - poniżej średniej oceny
        },
        new Rating
        {
            Note = 1  // 1/5 - niska ocena
        }
    };

                await context.RatingsDb.AddRangeAsync(ratings);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Survey
            // Sprawdzanie i dodawanie testowych rekordów dla Survey
            if (!context.SurveysDb.Any())
            {
                var surveys = new List<Survey>()
    {
        new Survey
        {
            Heigth = 175.5f,
            Weith = 70.2f,
            MeasureTime = DateTime.Now.AddDays(-10)
        },
        new Survey
        {
            Heigth = 160.4f,
            Weith = 60.1f,
            MeasureTime = DateTime.Now.AddDays(-9)
        },
        new Survey
        {
            Heigth = 182.7f,
            Weith = 80.5f,
            MeasureTime = DateTime.Now.AddDays(-8)
        },
        new Survey
        {
            Heigth = 168.2f,
            Weith = 65.3f,
            MeasureTime = DateTime.Now.AddDays(-7)
        },
        new Survey
        {
            Heigth = 170.0f,
            Weith = 72.4f,
            MeasureTime = DateTime.Now.AddDays(-6)
        },
        new Survey
        {
            Heigth = 177.8f,
            Weith = 74.2f,
            MeasureTime = DateTime.Now.AddDays(-5)
        },
        new Survey
        {
            Heigth = 174.3f,
            Weith = 68.9f,
            MeasureTime = DateTime.Now.AddDays(-4)
        },
        new Survey
        {
            Heigth = 162.6f,
            Weith = 63.7f,
            MeasureTime = DateTime.Now.AddDays(-3)
        },
        new Survey
        {
            Heigth = 179.1f,
            Weith = 76.0f,
            MeasureTime = DateTime.Now.AddDays(-2)
        },
        new Survey
        {
            Heigth = 165.5f,
            Weith = 62.5f,
            MeasureTime = DateTime.Now.AddDays(-1)
        },
    };

                await context.SurveysDb.AddRangeAsync(surveys);
                await context.SaveChangesAsync();
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
                await context.SaveChangesAsync();
            }
            #endregion

            //********************DANE Z KLUCZAMI OBCYMI****************************************

            #region SingleDiet
            // Sprawdzanie i dodawanie testowych rekordów dla SingleDietsDb
            if (!context.SingleDietsDb.Any())
            {
                var singleDiets = new List<SingleDiet>()
                {
                    new SingleDiet
                    {
                        MealTimeHour = 8,
                        MealTimeMinute = 0,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddDays(7),
                        DayWeekId = 1  // Poniedziałek
                    },
                    new SingleDiet
                    {
                        MealTimeHour = 12,
                        MealTimeMinute = 30,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddDays(7),
                        DayWeekId = 2  // Wtorek
                    },
                    new SingleDiet
                    {
                        MealTimeHour = 14,
                        MealTimeMinute = 15,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddDays(7),
                        DayWeekId = 3  // Środa
                    },
                    new SingleDiet
                    {
                        MealTimeHour = 19,
                        MealTimeMinute = 45,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddDays(7),
                        DayWeekId = 4  // Czwartek
                    },
                    new SingleDiet
                    {
                        MealTimeHour = 9,
                        MealTimeMinute = 0,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddDays(7),
                        DayWeekId = 5  // Piątek
                    },
                    new SingleDiet
                    {
                        MealTimeHour = 12,
                        MealTimeMinute = 15,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddDays(7),
                        DayWeekId = 6  // Sobota
                    },
                    new SingleDiet
                    {
                        MealTimeHour = 10,
                        MealTimeMinute = 30,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddDays(7),
                        DayWeekId = 7  // Niedziela
                    },
                };

                await context.SingleDietsDb.AddRangeAsync(singleDiets);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Dietician
            // Sprawdzanie i dodawanie testowych rekordów dla Dietician
            if (!context.DieticiansDb.Any())
            {
                var dieticians = new List<Dietician>()
                {
                    new Dietician
                    {
                        FirstName = "Anna",
                        LastName = "Nowak",
                        Email = "anna.nowak1@example.com",
                        Password = "password123",
                        Phone = "987654321",
                        isPatient = false,
                        isDietician = true,
                        isAdmin = false,
                        BirthDate = new DateTime(1985, 2, 2),
                        AddressId = 1,
                        RatingId = 1
                    },
                    new Dietician
                    {
                        FirstName = "Marek",
                        LastName = "Kowalski",
                        Email = "marek.kowalski@example.com",
                        Password = "password123",
                        Phone = "987654322",
                        isPatient = false,
                        isDietician = true,
                        isAdmin = false,
                        BirthDate = new DateTime(1984, 3, 3),
                        AddressId = 2,
                        RatingId = 2
                    },
                    new Dietician
                    {
                        FirstName = "Oliwia",
                        LastName = "Wiśniewska",
                        Email = "oliwia.wisniewska@example.com",
                        Password = "password123",
                        Phone = "987654330",
                        isPatient = false,
                        isDietician = true,
                        isAdmin = false,
                        BirthDate = new DateTime(1990, 10, 10),
                        AddressId = 10,
                        RatingId = 5
                    }
                };

                await context.DieticiansDb.AddRangeAsync(dieticians);
            }
            #endregion
            #region PatientCard
            // Sprawdzanie i dodawanie testowych rekordów dla PatientCard
            if (!context.PatientCardsDb.Any())
            {
                var patientCards = new List<PatientCard>()
                {
                    new PatientCard
                    {

                        SexId = 1
                    },
                    new PatientCard
                    {

                        SexId = 2
                    },
                    new PatientCard
                    {

                        SexId = 1
                    },
                    new PatientCard
                    {

                        SexId = 2
                    },
                    new PatientCard
                    {

                        SexId = 2
                    }
                };

                await context.PatientCardsDb.AddRangeAsync(patientCards);
            }
            #endregion
            #region Pacjent
            // Sprawdzanie i dodawanie testowych rekordów dla Pacjenta
            if (!context.PatientsDb.Any())
            {
                var pacjenci = new List<Patient>()
                {
                    new Patient
                    {
                        FirstName = "Tomasz",
                        LastName = "Zieliński",
                        Email = "tomasz.zielinski@example.com",
                        Password = "haslo123",
                        Phone = "500600700",
                        isPatient = true,
                        isDietician = false,
                        isAdmin = false,
                        BirthDate = new DateTime(1985, 6, 15),
                        AddressId = 1,
                        PatientCardId = 1,
                        // Poniższe listy zostały już wypełnione i są dostępne.
                        Notes = new List<Note>(),
                        Dieticians = new List<Dietician>(),
                        Comments = new List<Comment>(),
                        Ratings = new List<Rating>(),
                        Visits = new List<Visit>(),
                        MessagePatients = new List<MessagePatient>(),
                        DieticianPatients = new List<DieticianPatient>(),
                        DietPatients = new List<DietPatient>(),
                        NotePatients = new List<NotePatient>()
                    },
                    new Patient
                    {
                        FirstName = "Aleksandra",
                        LastName = "Nowak",
                        Email = "aleksandra.nowak@example.com",
                        Password = "haslo123",
                        Phone = "501601701",
                        isPatient = true,
                        isDietician = false,
                        isAdmin = false,
                        BirthDate = new DateTime(1990, 2, 20),
                        AddressId = 2,
                        PatientCardId = 2,
                        // Poniższe listy zostały już wypełnione i są dostępne.
                        Notes = new List<Note>(),
                        Dieticians = new List<Dietician>(),
                        Comments = new List<Comment>(),
                        Ratings = new List<Rating>(),
                        Visits = new List<Visit>(),
                        MessagePatients = new List<MessagePatient>(),
                        DieticianPatients = new List<DieticianPatient>(),
                        DietPatients = new List<DietPatient>(),
                        NotePatients = new List<NotePatient>()
                    },
                    new Patient
                    {
                        FirstName = "Piotr",
                        LastName = "Kowal",
                        Email = "piotr.kowal@example.com",
                        Password = "haslo123",
                        Phone = "502602702",
                        isPatient = true,
                        isDietician = false,
                        isAdmin = false,
                        BirthDate = new DateTime(1988, 5, 5),
                        AddressId = 3,
                        PatientCardId = 3,
                        // Poniższe listy zostały już wypełnione i są dostępne.
                        Notes = new List<Note>(),
                        Dieticians = new List<Dietician>(),
                        Comments = new List<Comment>(),
                        Ratings = new List<Rating>(),
                        Visits = new List<Visit>(),
                        MessagePatients = new List<MessagePatient>(),
                        DieticianPatients = new List<DieticianPatient>(),
                        DietPatients = new List<DietPatient>(),
                        NotePatients = new List<NotePatient>()
                    }
                };

                await context.PatientsDb.AddRangeAsync(pacjenci);
            }
            #endregion
            #region Admin
            if (!context.AdminsDb.Any())
            {
                var admins = new List<Admin>()

            {
                new Admin
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jan.kowalski@example.com",
                    Password = "securepassword1",
                    Phone = "500100200",
                    isPatient = false,
                    isDietician = false,
                    isAdmin = true,
                    BirthDate = new DateTime(1980, 1, 1),
                    AddressId = 1,
                    Notes = new List<Note>(),
                    MessageTo = new List<MessageTo>(),
                    MessageAdmins = new List<MessageAdmin>()
                },
                new Admin
                {
                    FirstName = "Ewa",
                    LastName = "Nowak",
                    Email = "ewa.nowak@example.com",
                    Password = "securepassword2",
                    Phone = "500100201",
                    isPatient = false,
                    isDietician = false,
                    isAdmin = true,
                    BirthDate = new DateTime(1982, 5, 10),
                    AddressId = 2,
                    Notes = new List<Note>(),
                    MessageTo = new List<MessageTo>(),
                    MessageAdmins = new List<MessageAdmin>()
                },
            };

                await context.AdminsDb.AddRangeAsync(admins);
            }
            #endregion

            //********************DANE Z KLUCZAMI KLUCZY OBCYCH****************************************
            #region MessagesToDietician
            //Sprawdzanie i dodawanie testowych wiadomości do dietetyka
            //if (!context.MessageToDb.Any())
            //{
            //    var messages = new List<MessageTo>()
            //{
            //    new MessageTo
            //    {
            //        Title = "Pytanie o dietę 1",
            //        Description = "Mam pytanie odnośnie ilości węglowodanów w diecie.",
            //        DieticianId = 1,
            //        PatientId=1,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    },
            //    new MessageTo
            //    {
            //        Title = "Kwestia alergii",
            //        Description = "Czy produkt X jest odpowiedni dla osoby z alergią na orzechy?",
            //        DieticianId = 2,
            //        PatientId=1,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    },
            //    new MessageTo
            //    {
            //        Title = "Porada dla sportowca",
            //        Description = "Jakie produkty zalecasz dla aktywnych fizycznie osób?",
            //        DieticianId = 3,
            //        PatientId=3,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    },
            //    new MessageTo
            //    {
            //        Title = "Suplementacja",
            //        Description = "Czy warto suplementować witaminę D w okresie zimowym?",
            //        DieticianId = 1,
            //        PatientId=2,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    },
            //    new MessageTo
            //    {
            //        Title = "Pytanie o jadłospis",
            //        Description = "Czy możesz mi pomóc skomponować jadłospis na nadchodzący tydzień?",
            //        DieticianId = 2,
            //        PatientId=2,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    }
            //};

            //    await context.MessageToDb.AddRangeAsync(messages);
            //}
            #endregion
            #region MessageToPatient

            // Sprawdzanie i dodawanie testowych wiadomości do pacjentów
            if (!context.MessageToDb.Any())
            {
                var messagesToPatients = new List<MessageTo>()
            {
                new MessageTo
                {
                    Title = "Konsultacja dietetyczna",
                    Description = "Witaj! Zapraszam na konsultację dietetyczną w przyszłym tygodniu. Daj mi znać, kiedy Ci pasuje.",
                    PatientId = 1,
                    AdminId=1,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Zmiana planu dietetycznego",
                    Description = "Witam! Zaktualizowałem Twój plan dietetyczny. Sprawdź go w aplikacji i daj mi znać, czy wszystko jest jasne.",
                    PatientId = 1,
                    AdminId=2,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Przypomnienie o wizycie",
                    Description = "Przypominam o jutrzejszej wizycie. Jeśli nie możesz przyjść, daj mi znać jak najszybciej.",
                    PatientId = 2,
                    AdminId=1,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Wyniki badań",
                    Description = "Twoje wyniki badań są już dostępne. Zalecam omówienie ich podczas najbliższej wizyty.",
                    PatientId = 2,
                    AdminId=2,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Zalecenia po wizycie",
                    Description = "Witaj! Po naszej ostatniej wizycie przygotowałem kilka zaleceń. Sprawdź je w aplikacji i postępuj zgodnie z nimi.",
                    PatientId = 3,
                    AdminId=2,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
            };

                await context.MessageToDb.AddRangeAsync(messagesToPatients);
            }

            #endregion

            //#region MessagesToAdmin
            //if (!context.MessageToDb.Any())
            //{
            //    var messagesToAdmins = new List<MessageTo>()
            //{
            //    new MessageTo
            //    {
            //        Title = "Zgłoszenie problemu technicznego",
            //        Description = "Witaj! Napotkaliśmy problem z funkcją wysyłania wiadomości w aplikacji. Prosimy o szybką interwencję.",
            //        AdminId = 1,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    },
            //    new MessageTo
            //    {
            //        Title = "Aktualizacja polityki prywatności",
            //        Description = "Witaj! Przesyłam aktualizację polityki prywatności. Proszę o jej przejrzenie i zatwierdzenie.",
            //        AdminId = 2,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    },
            //    new MessageTo
            //    {
            //        Title = "Propozycja współpracy",
            //        Description = "Dzień dobry! Mamy propozycję współpracy, która może być interesująca dla naszej platformy. Proszę o kontakt.",
            //        AdminId = 1,
            //        IsRead = false,
            //        ReadDate = null,
            //        dateAdded = DateTime.Now
            //    },
            //};

            //    await context.MessageToDb.AddRangeAsync(messagesToAdmins);
            //}
            //#endregion

            // Zapisanie zmian w bazie danych
            await context.SaveChangesAsync();
        }
    }
}
