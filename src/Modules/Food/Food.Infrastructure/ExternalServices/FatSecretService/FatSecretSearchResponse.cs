using System.Text.Json.Serialization;

namespace Food.Infrastructure.ExternalServices.FatSecretService
{
    internal sealed record FatSecretSearchResponse([property: JsonPropertyName("foods_search")] FoodsSearch? FoodsSearch);

    internal sealed record FoodsSearch(
        [property: JsonPropertyName("max_results")] string MaxResults,
        [property: JsonPropertyName("total_results")] string TotalResults,
        [property: JsonPropertyName("page_number")] string PageNumber,
        [property: JsonPropertyName("results")] FoodsResults? Results);

    internal sealed record FoodsResults([property: JsonPropertyName("food")] List<FatSecretFood> Food);

    internal sealed record FatSecretFood(
        [property: JsonPropertyName("food_id")] string FoodId,
        [property: JsonPropertyName("food_name")] string FoodName,
        [property: JsonPropertyName("food_images")] FoodImagesWrapper? FoodImages,
        [property: JsonPropertyName("servings")] ServingsWrapper? Servings,
        [property: JsonPropertyName("brand_name")] string? BrandName);

    internal sealed record FoodImagesWrapper([property: JsonPropertyName("food_image")] List<FoodImage> FoodImage);

    internal sealed record FoodImage([property: JsonPropertyName("image_url")] string ImageUrl);

    internal sealed record ServingsWrapper([property: JsonPropertyName("serving")] List<FatSecretServing> Serving);

    internal sealed record FatSecretServing(
        [property: JsonPropertyName("serving_description")] string ServingDescription,
        [property: JsonPropertyName("calories")] string Calories,
        [property: JsonPropertyName("carbohydrate")] string Carbohydrate,
        [property: JsonPropertyName("protein")] string Protein,
        [property: JsonPropertyName("fat")] string Fat,
        [property: JsonPropertyName("saturated_fat")] string SaturatedFat,
        [property: JsonPropertyName("polyunsaturated_fat")] string PolyunsaturatedFat,
        [property: JsonPropertyName("monounsaturated_fat")] string MonounsaturatedFat,
        [property: JsonPropertyName("cholesterol")] string Cholesterol,
        [property: JsonPropertyName("sodium")] string Sodium,
        [property: JsonPropertyName("potassium")] string Potassium,
        [property: JsonPropertyName("fiber")] string Fiber,
        [property: JsonPropertyName("sugar")] string Sugar,
        [property: JsonPropertyName("vitamin_a")] string VitaminA,
        [property: JsonPropertyName("vitamin_c")] string VitaminC,
        [property: JsonPropertyName("calcium")] string Calcium,
        [property: JsonPropertyName("iron")] string Iron);
}