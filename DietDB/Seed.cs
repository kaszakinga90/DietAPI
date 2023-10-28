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
            // Checking and adding fake addresses
            if (!context.Addresses.Any())
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

                await context.Addresses.AddRangeAsync(addresses);
            }
            #endregion
            #region CategoryOfDiet
            // Sprawdzanie i dodawanie fałszywych kategorii diet
            if (!context.CategoryOfDiet.Any())
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

                await context.CategoryOfDiet.AddRangeAsync(categoriesOfDiets);
            }
            #endregion
            #region DayWeek
            // Sprawdzanie i dodawanie danych dla dni tygodnia w DayWeek
            if (!context.DayWeek.Any())
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
                await context.DayWeek.AddRangeAsync(days);
            }
            #endregion
            #region Sex
            // Sprawdzanie i dodawanie fejkowych danych dla Sex
            if (!context.Sex.Any()) // Upewnij się, że używasz poprawnej nazwy właściwości DbSet dla "Sex" w swoim DbContext. Zakładam, że to "Sexes".
            {
                var sexes = new List<Sex>()
    {
        new Sex { Name = "Mężczyzna" },
        new Sex { Name = "Kobieta" },
    };

                await context.Sex.AddRangeAsync(sexes);
            }
            #endregion
            #region MealTimes
            // Sprawdzanie i dodawanie danych dla MealTimes
            if (!context.MealTime.Any())
            {
                var mealTimes = new List<MealTime>()
                {
                    new MealTime { DishTime = "Śniadanie" },
                    new MealTime { DishTime = "Drugie śniadanie" },
                    new MealTime { DishTime = "Obiad" },
                    new MealTime { DishTime = "Podwieczorek" },
                    new MealTime { DishTime = "Kolacja" },
                };
                await context.MealTime.AddRangeAsync(mealTimes);
            }
            #endregion
            #region Measure
            // Sprawdzanie i dodawanie fałszywych jednostek miary
            if (!context.Measures.Any())
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

                await context.Measures.AddRangeAsync(measures);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Message
            // Sprawdzanie i dodawanie fałszywych wiadomości
            if (!context.Messages.Any())
            {
                var messages = new List<Message>()
    {
        new Message
        {
            Title = "Witaj w systemie",
            Description = "Witaj w naszym systemie dietetycznym! Mamy nadzieję, że będzie Ci się z nami dobrze współpracować."
        },
        new Message
        {
            Title = "Nowe funkcje",
            Description = "Drodzy użytkownicy, właśnie wprowadziliśmy nowe funkcje do systemu. Zachęcamy do zapoznania się z nimi!"
        },
        new Message
        {
            Title = "Przypomnienie o wizycie",
            Description = "Przypominamy o zbliżającej się wizycie u dietetyka w najbliższy piątek o godzinie 15:00."
        },
        new Message
        {
            Title = "Zmiany w diecie",
            Description = "Informujemy, że wprowadziliśmy kilka zmian w rekomendowanej diecie. Proszę o zapoznanie się z aktualizacjami."
        },
        new Message
        {
            Title = "Ankieta satysfakcji",
            Description = "Zachęcamy do wypełnienia ankiety satysfakcji dotyczącej naszych usług. Twoja opinia jest dla nas bardzo ważna!"
        }
    };

                await context.Messages.AddRangeAsync(messages);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Rating
            // Sprawdzanie i dodawanie fałszywych ocen
            if (!context.Rating.Any())
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

                await context.Rating.AddRangeAsync(ratings);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Survey
            // Sprawdzanie i dodawanie fałszywych rekordów dla Survey
            if (!context.Surveys.Any())
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

                await context.Surveys.AddRangeAsync(surveys);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Term
            // Sprawdzanie i dodawanie fałszywych rekordów dla Term
            if (!context.Terms.Any())
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

                await context.Terms.AddRangeAsync(terms);
                await context.SaveChangesAsync();
            }
            #endregion

            //********************DANE Z KLUCZAMI OBCYMI****************************************

            #region SingleDiet
            // Sprawdzanie i dodawanie fałszywych rekordów dla SingleDiet
            if (!context.SingleDiet.Any())
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

                await context.SingleDiet.AddRangeAsync(singleDiets);
                await context.SaveChangesAsync();
            }
            #endregion
            #region Dietician
            // Sprawdzanie i dodawanie fałszywych rekordów dla Dietician
            if (!context.Dieticians.Any())
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
            PhotoUrl = "http://example.com/photo1.jpg",
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
            PhotoUrl = "http://example.com/photo2.jpg",
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
            PhotoUrl = "http://example.com/photo10.jpg",
            isPatient = false,
            isDietician = true,
            isAdmin = false,
            BirthDate = new DateTime(1990, 10, 10),
            AddressId = 10,
            RatingId = 5
        }
    };

                await context.Dieticians.AddRangeAsync(dieticians);
            }
            #endregion
            #region PatientCard
            // Sprawdzanie i dodawanie fałszywych rekordów dla PatientCard
            if (!context.PatientCards.Any())
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

                await context.PatientCards.AddRangeAsync(patientCards);
            }
            #endregion
            #region Pacjent
            // Sprawdzanie i dodawanie fałszywych rekordów dla Pacjenta
            if (!context.Patients.Any())
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
            PhotoUrl = "https://przyklad.com/zdjecia/tomasz_zielinski.jpg",
            isPatient = true,
            isDietician = false,
            isAdmin = false,
            BirthDate = new DateTime(1985, 6, 15),
            AddressId = 1,
            PatientCardId = 1,
            // Zakładam, że odpowiednie listy zostały już wypełnione i są dostępne.
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
            PhotoUrl = "https://przyklad.com/zdjecia/aleksandra_nowak.jpg",
            isPatient = true,
            isDietician = false,
            isAdmin = false,
            BirthDate = new DateTime(1990, 2, 20),
            AddressId = 2,
            PatientCardId = 2,
            // Zakładam, że odpowiednie listy zostały już wypełnione i są dostępne.
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
            PhotoUrl = "https://przyklad.com/zdjecia/piotr_kowal.jpg",
            isPatient = true,
            isDietician = false,
            isAdmin = false,
            BirthDate = new DateTime(1988, 5, 5),
            AddressId = 3,
            PatientCardId = 3,
            // Zakładam, że odpowiednie listy zostały już wypełnione i są dostępne.
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

                await context.Patients.AddRangeAsync(pacjenci);
            }
            #endregion

            //********************DANE Z KLUCZAMI KLUCZY OBCYCH****************************************
            #region MessagesToDietician
            // Checking and adding fake messages to dietician
            if (!context.MessageToDieticians.Any())
            {
                var messages = new List<MessageToDietician>()
    {
        new MessageToDietician
        {
            Title = "Pytanie o dietę 1",
            Description = "Mam pytanie odnośnie ilości węglowodanów w diecie.",
            DieticianId = 1,
            PatientId=1,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToDietician
        {
            Title = "Kwestia alergii",
            Description = "Czy produkt X jest odpowiedni dla osoby z alergią na orzechy?",
            DieticianId = 2,
            PatientId=1,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToDietician
        {
            Title = "Porada dla sportowca",
            Description = "Jakie produkty zalecasz dla aktywnych fizycznie osób?",
            DieticianId = 3,
            PatientId=3,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToDietician
        {
            Title = "Suplementacja",
            Description = "Czy warto suplementować witaminę D w okresie zimowym?",
            DieticianId = 1,
            PatientId=2,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToDietician
        {
            Title = "Pytanie o jadłospis",
            Description = "Czy możesz mi pomóc skomponować jadłospis na nadchodzący tydzień?",
            DieticianId = 2,
            PatientId=2,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        }
    };

                await context.MessageToDieticians.AddRangeAsync(messages);
            }
            #endregion

            #region MessageToPatient

            // Sprawdzanie i dodawanie realistycznych wiadomości do pacjentów
            if (!context.MessageToPatients.Any())
            {
                var messagesToPatients = new List<MessageToPatient>()
    {
        new MessageToPatient
        {
            Title = "Konsultacja dietetyczna",
            Description = "Witaj! Zapraszam na konsultację dietetyczną w przyszłym tygodniu. Daj mi znać, kiedy Ci pasuje.",
            PatientId = 1,
            DieticianId=1,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToPatient
        {
            Title = "Zmiana planu dietetycznego",
            Description = "Witam! Zaktualizowałem Twój plan dietetyczny. Sprawdź go w aplikacji i daj mi znać, czy wszystko jest jasne.",
            PatientId = 1,
            DieticianId=3,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToPatient
        {
            Title = "Przypomnienie o wizycie",
            Description = "Przypominam o jutrzejszej wizycie. Jeśli nie możesz przyjść, daj mi znać jak najszybciej.",
            PatientId = 2,
            DieticianId=1,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToPatient
        {
            Title = "Wyniki badań",
            Description = "Twoje wyniki badań są już dostępne. Zalecam omówienie ich podczas najbliższej wizyty.",
            PatientId = 2,
            DieticianId=2,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
        new MessageToPatient
        {
            Title = "Zalecenia po wizycie",
            Description = "Witaj! Po naszej ostatniej wizycie przygotowałem kilka zaleceń. Sprawdź je w aplikacji i postępuj zgodnie z nimi.",
            PatientId = 3,
            DieticianId=2,
            IsRead = false,
            ReadDate = null,
            dateAdded = DateTime.Now
        },
    };

                await context.MessageToPatients.AddRangeAsync(messagesToPatients);
            }

            #endregion


            // Zapisanie zmian w bazie danych
            await context.SaveChangesAsync();
        }
    }
}
