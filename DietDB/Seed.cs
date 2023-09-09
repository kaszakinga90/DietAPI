using ModelsDB;

namespace DietDB
{
    public class Seed
    {
        public static async Task SeedData(DietContext context)
        {
            if (context.Examples.Any()) return;

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
            await context.SaveChangesAsync();
        }
    }
}
