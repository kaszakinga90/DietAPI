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
            // Sprawdzanie i dodawanie fałszywych danych dla Examples
            if (!context.Examples.Any())
            {
                var examp = new List<Example>()
                {
                    new Example
                    {
                        Name="Arek",
                        Description="Jakis opis1",
                        Age=22,
                    },
                    new Example
                    {
                        Name="Iwona",
                        Description="Jakis opis2",
                        Age=18,
                    },
                    new Example
                    {
                        Name="Marcin",
                        Description="Jakis opis3",
                        Age=44,
                    },
                    new Example
                    {
                        Name="Kamila",
                        Description="Jakis opis4",
                        Age=28,
                    },
                };
                await context.Examples.AddRangeAsync(examp);
            }

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
            // Sprawdzanie i dodawanie danych dla CategoryOfDiet
            if (!context.CategoryOfDiet.Any())
            {
                var categories = new List<CategoryOfDiet>()
                {
                    new CategoryOfDiet { CategoryName = "Wegetariańska" },
                    new CategoryOfDiet { CategoryName = "Wegańska" },
                    new CategoryOfDiet { CategoryName = "Ketogeniczna" },
                    new CategoryOfDiet { CategoryName = "Paleo" },
                    new CategoryOfDiet { CategoryName = "Dieta niskowęglowodanowa" }
                };
                await context.CategoryOfDiet.AddRangeAsync(categories);
            }
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
            // Sprawdzanie i dodawanie danych dla dni tygodnia w SingleDiet
            if (!context.SingleDiet.Any())
            {
                DateTime baseDate = DateTime.Now;

                for (int i = 0; i < 26; i++) // dla 26 dni
                {
                    int dayOfWeekId = (i % 7) + 1; // Zakładamy, że wartości dla DayWeekId są od 1 do 7.
                    DateTime startDate = baseDate.AddDays(i);
                    DateTime endDate = startDate.AddHours(24);
                    var categories = context.CategoryOfDiet.Where(c => c.Id >= 1 && c.Id <= 5).ToList();

                    var singleDiet1 = new SingleDiet
                    {
                        MealTimeHour = 10, // Przykładowe wartości
                        MealTimeMinute = 30,
                        DateStart = startDate,
                        DateEnd = endDate,
                        DayWeekId = dayOfWeekId,
                        DietCategories = categories
                    };

                    var singleDiet2 = new SingleDiet
                    {
                        MealTimeHour = 12, // Przykładowe wartości
                        MealTimeMinute = 45,
                        DateStart = startDate,
                        DateEnd = endDate,
                        DayWeekId = dayOfWeekId,
                        DietCategories = categories
                    };

                    await context.SingleDiet.AddRangeAsync(singleDiet1, singleDiet2);
                }
            }
                // Zapisanie zmian w bazie danych
                await context.SaveChangesAsync();
        }
    }
}
