using Application.CQRS.Ingredients;
using Application.DTOs.IngredientDTO;
using Application.Services;
using DietDB;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
    public class NutritionixController : BaseApiController
    {
        private readonly ImageService _imageService;
        private readonly DietContext _context;

        public NutritionixController(ImageService imageService, DietContext context)
        {
            _imageService = imageService;
            _context = context;
        }

        [HttpGet("{productName}")]
        //[Route("product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductWithNutrients(string productName)
        {
            try
            {
                string apiUrl = "https://trackapi.nutritionix.com/v2/search/instant";
                string appIdNutritionix = "a9a046a7";
                string appKeyNutritionix = "6b04ebbeff3b64533972f54009f93549";

                var units = _context.UnitsDb.ToDictionary(u => u.Symbol, u => u.Id);
                var measures = _context.MeasuresDb.ToDictionary(m => m.Symbol, m => m.Id);

                using (HttpClient client = new HttpClient())
                {
                    // Dodanie nagłówków wymaganych przez API Nutritionix
                    client.DefaultRequestHeaders.Add("x-app-id", appIdNutritionix);
                    client.DefaultRequestHeaders.Add("x-app-key", appKeyNutritionix);

                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}?query={productName}&self=false&branded=true&branded_food_name_only=false&common=true&common_general=true&common_grocery=true&common_restaurant=true&locale=pl_PL&detailed=true&claims=false&taxonomy=false");

                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                        using (JsonDocument doc = await JsonDocument.ParseAsync(responseStream))
                        {
                            var root = doc.RootElement;

                            if (root.TryGetProperty("common", out var commonElement) && commonElement.ValueKind == JsonValueKind.Array)
                            {
                                var ingredientDTOs = new List<IngredientDTO>();

                                foreach (var element in commonElement.EnumerateArray())
                                {
                                    var measureName = element.GetProperty("serving_unit").GetString();

                                    ////Sprawdzenie, czy jednostka miary istnieje w kolekcji wcześniej pobranych jednostek miary
                                    //if (measures.TryGetValue(measureName, out var measureId))
                                    //{
                                    //    // Jednostka miary istnieje, możemy użyć jej identyfikatora
                                    //}
                                    //else
                                    //{
                                    //    var newMeasure = new Measure
                                    //    {
                                    //        Symbol = measureName,
                                    //    };

                                    //    _context.MeasuresDb.Add(newMeasure);
                                    //    await _context.SaveChangesAsync();

                                    //    // Pobierz identyfikator nowo dodanej jednostki miary
                                    //    measureId = newMeasure.Id;

                                    //    measures.Add(measureName, measureId);
                                    //}

                                    // Sprawdzenie, czy jednostka istnieje w kolekcji wcześniej pobranych jednostek
                                    //if (units.TryGetValue("g", out var unitId))
                                    //{
                                    //    // Jednostka istnieje, możemy użyć jej identyfikatora
                                    //}
                                    //else
                                    //{
                                    //    // Jednostka nie istnieje, dodaj ją do bazy danych
                                    //    var newUnit = new Unit
                                    //    {
                                    //        Symbol = "g",
                                    //    };

                                    //    _context.UnitsDb.Add(newUnit);
                                    //    await _context.SaveChangesAsync();

                                    //    // Pobierz identyfikator nowo dodanej jednostki miary
                                    //    unitId = newUnit.Id;

                                    //    units.Add(newUnit.Symbol, unitId);
                                    //}

                                    var ingredientDTO = new IngredientDTO
                                    {
                                        Name = element.GetProperty("food_name").GetString(),
                                        NameEN = element.GetProperty("tag_name").GetString(),
                                        Calories = element.GetProperty("full_nutrients")
                                                        .EnumerateArray()
                                                        .FirstOrDefault(nutrient => nutrient.GetProperty("attr_id").GetInt32() == 208)
                                                        .GetProperty("value")
                                                        .GetSingle(),
                                        ServingQuantity = element.GetProperty("serving_qty").GetSingle(),
                                        MeasureId = null, // TODO: po kliknięciu konkretnego produktu  należy wysłać zap do bazy aby sprawdzic i przypisac odpowiednią jednostkę
                                        MeasureNameFromJSON = measureName,
                                        Weight = element.GetProperty("serving_weight_grams").GetSingle(),
                                        UnitId = 1,
                                        PictureUrl = element.GetProperty("photo").GetProperty("thumb").GetString(),
                                    };

                                    ingredientDTO.LoadNutrientsLazy(element.GetProperty("full_nutrients"));
                                    ingredientDTOs.Add(ingredientDTO);
                                }

                                return Ok(ingredientDTOs);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Błąd: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania danych: {ex.Message}");
            }
            // Zwróć pustą listę, jeśli nie znaleziono produktów lub wystąpił błąd
            return Ok(new List<IngredientDTO>());
        }

        /// <summary>
        /// Tworzy nowy produkt.
        /// </summary>
        /// <param name="Ingredient">Dane produktu do utworzenia.</param>
        /// <returns>Status operacji.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateIngredientFromNutritionix(IngredientDTO Ingredient)
        {
            await Mediator.Send(new IngredientFromNutritionixCreate.Command { IngredientDTO = Ingredient });
            return Ok();
        }
    }

    public static class IngredientDTOExtensions
    {
        public static void LoadNutrientsLazy(this IngredientDTO ingredientDTO, JsonElement nutrientsArray)
        {
            var nutrientsDTOList = new List<IngredientNutrientDTO>();

            foreach (var nutrientElement in nutrientsArray.EnumerateArray())
            {
                var attrId = nutrientElement.GetProperty("attr_id").GetInt32();
                var nutrientDTO = new IngredientNutrientDTO
                {
                    NutrientId = attrId,
                    NutrientValue = nutrientElement.GetProperty("value").GetSingle()
                };
                nutrientsDTOList.Add(nutrientDTO);
            }

            if (ingredientDTO.NutrientsDTO == null)
            {
                ingredientDTO.NutrientsDTO = new List<IngredientNutrientDTO>();
            }

            ingredientDTO.NutrientsDTO.AddRange(nutrientsDTOList);

            foreach (var nutrientDTO in nutrientsDTOList)
            {
                Console.WriteLine($"NutrientId: {nutrientDTO.NutrientId}, NutrientValue: {nutrientDTO.NutrientValue}");
            }
        }
    }
}
