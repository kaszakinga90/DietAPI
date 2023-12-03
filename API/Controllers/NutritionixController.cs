using Application.CQRS.Ingredients.Nutritionixs;
using Application.DTOs.IngredientDTO.IngredientNutritionixDTO;
using Application.Services;
using DietDB;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using ModelsDB.Functionality;
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

        [HttpGet("ingredient/{productName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductWithNutrients(string productName)
        {
            try
            {
                string apiUrl = "https://trackapi.nutritionix.com/v2/search/instant";
                string appIdNutritionix = "724e67dc";
                string appKeyNutritionix = "774b737e8771633bb04f37ce0abf814b";

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
                                var ingredientDTOs = new List<IngredientNutritionixDTO>();

                                foreach (var element in commonElement.EnumerateArray())
                                {
                                    var measureName = element.GetProperty("serving_unit").GetString();

                                    //Sprawdzenie, czy jednostka miary istnieje w kolekcji wcześniej pobranych jednostek miary
                                    if (measures.TryGetValue(measureName, out var measureId))
                                    {
                                        // Jednostka miary istnieje, możemy użyć jej identyfikatora
                                    }
                                    else
                                    {
                                        var newMeasure = new Measure
                                        {
                                            Symbol = measureName,
                                        };

                                        _context.MeasuresDb.Add(newMeasure);
                                        await _context.SaveChangesAsync();

                                        // Pobierz identyfikator nowo dodanej jednostki miary
                                        measureId = newMeasure.Id;

                                        measures.Add(measureName, measureId);
                                    }

                                    // Sprawdzenie, czy jednostka istnieje w kolekcji wcześniej pobranych jednostek
                                    if (units.TryGetValue("g", out var unitId))
                                    {
                                        // Jednostka istnieje, możemy użyć jej identyfikatora
                                    }
                                    else
                                    {
                                        // Jednostka nie istnieje, dodaj ją do bazy danych
                                        var newUnit = new Unit
                                        {
                                            Symbol = "g",
                                        };

                                        _context.UnitsDb.Add(newUnit);
                                        await _context.SaveChangesAsync();

                                        // Pobierz identyfikator nowo dodanej jednostki miary
                                        unitId = newUnit.Id;

                                        units.Add(newUnit.Symbol, unitId);
                                    }

                                    var ingredientDTO = new IngredientNutritionixDTO
                                    {
                                        Name = element.GetProperty("food_name").GetString(),
                                        NameEN = element.GetProperty("tag_name").GetString(),
                                        Calories = element.GetProperty("full_nutrients")
                                                        .EnumerateArray()
                                                        .FirstOrDefault(nutrient => nutrient.GetProperty("attr_id").GetInt32() == 208)
                                                        .GetProperty("value")
                                                        .GetSingle(),
                                        ServingQuantity = element.GetProperty("serving_qty").GetSingle(),
                                        MeasureId = measureId,
                                        MeasureNameFromJSON = measureName,
                                        Weight = element.GetProperty("serving_weight_grams").GetSingle(),
                                        UnitId = unitId,
                                        PictureUrl = element.GetProperty("photo").GetProperty("thumb").GetString(),
                                    };

                                    ingredientDTO.LoadNutrientsLazy(element.GetProperty("full_nutrients"), _context);
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
            return Ok(new List<IngredientNutritionixDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientFromNutritionix(IngredientNutritionixDTO Ingredient)
        {
            await Mediator.Send(new IngredientFromNutritionixCreate.Command { IngredientNutritionixDTO = Ingredient });
            return Ok();
        }

    }

    public static class IngredientDTOExtensions
    {
        public static void LoadNutrientsLazy(this IngredientNutritionixDTO ingredientDTO, JsonElement nutrientsArray, DietContext context)
        {
            var nutrientsDTOList = new List<IngredientNutrientDTO>();
            var nutrientDictionary = context.NutrientsDb.ToDictionary(n => n.NutritionixId);

            foreach (var nutrientElement in nutrientsArray.EnumerateArray())
            {
                var attrId = nutrientElement.GetProperty("attr_id").GetInt32();

                if (nutrientDictionary.TryGetValue(attrId, out var existingNutrient))
                {
                    var nutrientDTO = new IngredientNutrientDTO
                    {
                        NutrientId = existingNutrient.Id,
                        NutrientValue = nutrientElement.GetProperty("value").GetSingle()
                    };
                    nutrientsDTOList.Add(nutrientDTO);
                }
                else
                {
                    Console.WriteLine("OOOOPS..!  Chyba nie ma takiego składnika (Nutrition)");
                    // TODO:  ??? Jeśli nutrient o danym NutritionixId nie istnieje w lokalnym słowniku,
                }
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
 