using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Food.Infrastructure.ExternalServices.FatSecretService
{
    public sealed class FatSecretTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<FatSecretSettings> _options;
        private readonly ILogger<FatSecretTokenService> _logger;
        private readonly SemaphoreSlim _lock = new (1,1);

        private string? _cachedToken;
        private DateTime _tokenExpiry = DateTime.MinValue;

        public FatSecretTokenService(
            IHttpClientFactory httpClientFactory,
            IOptions<FatSecretSettings> options,
            ILogger<FatSecretTokenService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("FatSecret");
            _options = options;
            _logger = logger;
        }

        public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
        {
            if (_cachedToken != null && DateTime.UtcNow < _tokenExpiry)
                return _cachedToken;

            await _lock.WaitAsync(cancellationToken);
            try
            {
                if (_cachedToken != null && DateTime.UtcNow < _tokenExpiry)
                    return _cachedToken;

                var body = new FormUrlEncodedContent([
                    new KeyValuePair<string, string>("grant_type", _options.Value.GrantType),
                    new KeyValuePair<string, string>("client_id", _options.Value.ClientId),
                    new KeyValuePair<string, string>("client_secret", _options.Value.ClientSecret),
                    new KeyValuePair<string, string>("scope", _options.Value.Scopes)
                ]);

                _logger.LogDebug("token not found, getting a new one");
                var response = await _httpClient.PostAsync(_options.Value.TokenUrl, body, cancellationToken);
                response.EnsureSuccessStatusCode();

                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
                if (tokenResponse is null)
                    throw new Exception("just have a tokenResponse bro, brutal 🥀");

                _cachedToken = tokenResponse.AccessToken;
                _tokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 120);

                return _cachedToken;
            }
            finally
            {
                _lock.Release();
            }
        }

        public sealed record TokenResponse(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("expires_in")] int ExpiresIn);
    }
}