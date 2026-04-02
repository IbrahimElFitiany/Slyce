using Food.Application.DTOs;
using Food.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Food.Infrastructure.ExternalServices.FatSecretService
{
    public sealed class FatSecretFoodService : IExternalFoodService
    {
        private readonly HttpClient _httpClient;
        private readonly FatSecretTokenService _tokenService;
        private readonly IOptions<FatSecretSettings> _options;
        private readonly ILogger<FatSecretFoodService> _logger;

        public FatSecretFoodService(
            IHttpClientFactory httpClientFactory,
            FatSecretTokenService tokenService,
            IOptions<FatSecretSettings> options,
            ILogger<FatSecretFoodService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("FatSecret");
            _tokenService = tokenService;
            _options = options;
            _logger = logger;
        }

        public async Task<IEnumerable<ExternalFoodResultDTO>> SearchAsync(string searchTerm, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetTokenAsync(cancellationToken);

            ///modify this shit fuck 
            var uri = $"{_options.Value.BaseUrl}{Uri.EscapeDataString(searchTerm)}&format=json&include_food_images=true";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            _logger.LogDebug("Sending food search request for: {SearchTerm}", searchTerm);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FatSecretSearchResponse>(cancellationToken);
            
            return result?.FoodsSearch?.Results?.Food
                .Select(f => (Food: f, Serving: f.Servings?.Serving.FirstOrDefault(s => s.ServingDescription == "100 g")))
                .Where(x => x.Serving is not null)
                .Select(x => MapToDto(x.Food, x.Serving!))
                .ToList() ?? [];
        }

        private static ExternalFoodResultDTO MapToDto(FatSecretFood f, FatSecretServing s) => new(
            Name: f.FoodName,
            ImageUrl: f.FoodImages?.FoodImage?.FirstOrDefault(i => i.ImageUrl?.Contains("400") == true)?.ImageUrl ?? string.Empty,
            ExternalId: f.FoodId,
            Source: "fatsecret",
            Calories: Parse(s.Calories),
            Protein: Parse(s.Protein),
            Carbs: Parse(s.Carbohydrate),
            Fat: Parse(s.Fat),
            SaturatedFat: Parse(s.SaturatedFat),
            TransFat: 0m,
            Cholesterol: Parse(s.Cholesterol),
            SodiumMg: Parse(s.Sodium),
            DietaryFiber: Parse(s.Fiber),
            SugarGrams: Parse(s.Sugar),
            PotassiumMg: Parse(s.Potassium),
            CalciumMg: Parse(s.Calcium),
            IronMg: Parse(s.Iron),
            VitaminAMcg: Parse(s.VitaminA),
            VitaminCMg: Parse(s.VitaminC),
            VitaminD: 0m
        );

        private static decimal Parse(string? value) =>  decimal.TryParse(value, out var result) ? result : 0m;
    }
}