﻿using ModelsDB;
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
                var mealTimes = new List<MealTimeToXYAxis>()
                {
                    new MealTimeToXYAxis { Name = "Śniadanie", MealTime = new DateTime(2023, 11, 10) },
                    new MealTimeToXYAxis { Name = "Drugie śniadanie", MealTime = new DateTime(2023, 11, 11) },
                    new MealTimeToXYAxis { Name = "Obiad", MealTime = new DateTime(2023, 11, 12) },
                    new MealTimeToXYAxis { Name = "Podwieczorek", MealTime = new DateTime(2023, 11, 13) },
                    new MealTimeToXYAxis { Name = "Kolacja", MealTime = new DateTime(2023, 11, 14) },
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
                    new Measure { Symbol = "Piece", Description = "Each" },
                    new Measure { Symbol = "Cup", Description = "Cup" },
                    new Measure { Symbol = "Teaspoon", Description = "Teaspoon" },
                    new Measure { Symbol = "Tablespoon", Description = "Tablespoon" },
                    new Measure { Symbol = "Ounce", Description = "Ounce" },
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
                await context.SaveChangesAsync();
            }
            #endregion

            //********************DANE Z KLUCZAMI OBCYMI****************************************

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
                        DieticianPatients = new List<DieticianPatient>(),
                        //DietPatients = new List<DietPatient>(),
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
                        DieticianPatients = new List<DieticianPatient>(),
                        //DietPatients = new List<DietPatient>(),
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
                        DieticianPatients = new List<DieticianPatient>(),
                        //DietPatients = new List<DietPatient>(),
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
                    MessageTo = new List<MessageTo>()
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
                    MessageTo = new List<MessageTo>()
                },
            };

                await context.AdminsDb.AddRangeAsync(admins);
            }
            #endregion

            //********************DANE Z KLUCZAMI KLUCZY OBCYCH****************************************

            #region MessagesToDietician
            //Sprawdzanie i dodawanie testowych wiadomości do dietetyka
            if (!context.MessageToDb.Any())
            {
                var messages = new List<MessageTo>()
            {
                new MessageTo
                {
                    Title = "Pytanie o dietę 1",
                    Description = "Mam pytanie odnośnie ilości węglowodanów w diecie.",
                    DieticianId = 1,
                    PatientId=1,
                    IsRead = false,
                    ReadDate = null,
                    isActive = true,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Kwestia alergii",
                    Description = "Czy produkt X jest odpowiedni dla osoby z alergią na orzechy?",
                    DieticianId = 2,
                    PatientId=1,
                    IsRead = false,
                    ReadDate = null,
                    isActive = true,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Porada dla sportowca",
                    Description = "Jakie produkty zalecasz dla aktywnych fizycznie osób?",
                    DieticianId = 3,
                    PatientId=3,
                    IsRead = false,
                    ReadDate = null,
                    isActive = true,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Suplementacja",
                    Description = "Czy warto suplementować witaminę D w okresie zimowym?",
                    DieticianId = 1,
                    PatientId=2,
                    IsRead = false,
                    ReadDate = null,
                    isActive = true,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Pytanie o jadłospis",
                    Description = "Czy możesz mi pomóc skomponować jadłospis na nadchodzący tydzień?",
                    DieticianId = 2,
                    PatientId=2,
                    IsRead = false,
                    ReadDate = null,
                    isActive = true,
                    dateAdded = DateTime.Now
                }
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
            #region MessagesToAdmin
            if (!context.MessageToDb.Any())
            {
                var messagesToAdmins = new List<MessageTo>()
            {
                new MessageTo
                {
                    Title = "Zgłoszenie problemu technicznego",
                    Description = "Witaj! Napotkaliśmy problem z funkcją wysyłania wiadomości w aplikacji. Prosimy o szybką interwencję.",
                    AdminId = 1,
                    DieticianId = 2,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Aktualizacja polityki prywatności",
                    Description = "Witaj! Przesyłam aktualizację polityki prywatności. Proszę o jej przejrzenie i zatwierdzenie.",
                    AdminId = 2,
                    DieticianId = 3,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
                new MessageTo
                {
                    Title = "Propozycja współpracy",
                    Description = "Dzień dobry! Mamy propozycję współpracy, która może być interesująca dla naszej platformy. Proszę o kontakt.",
                    AdminId = 1,
                    DieticianId = 1,
                    isActive = true,
                    IsRead = false,
                    ReadDate = null,
                    dateAdded = DateTime.Now
                },
            };

                await context.MessageToDb.AddRangeAsync(messagesToAdmins);
            }
            #endregion

            // TODO: do wprowadzenia na podst skryptu z bazy
            //#region Ingredients
            ////if (!context.IngredientsDb.Any())
            ////{
            ////    var ingredients = new List<Ingredient>()
            ////{
            ////    new Ingredient
            ////        {
            ////            NamePL = "Jajko",
            ////            NameEN = "Egg",
            ////            Calories = 70.0f,
            ////            ServingQuantity = 1.0f,
            ////            MeasureId = 1, // TODO: Replace with the appropriate measure ID
            ////            Weight = 50.0f,
            ////            UnitId = 2, // TODO: Replace with the appropriate unit ID
            ////            GlycemicIndex = 10,
            ////            PublicId = "123",
            ////            PictureUrl = "https://example.com/egg.jpg",
            ////            Nutrients = new List<IngredientNutrient>
            ////            {
            ////                new IngredientNutrient
            ////                {
            ////                    NutrientId = 1, // TODO: Replace with the appropriate nutrient ID
            ////                    NutrientValue = 5.0f
            ////                },
            ////                // Add more nutrients as needed
            ////            }
            ////        },
            ////        // Add more ingredients as needed

            ////};

            ////    await context.IngredientsDb.AddRangeAsync(ingredients);
            ////}
            //#endregion

            // Zapisanie zmian w bazie danych
            await context.SaveChangesAsync();
        }
    }
}
