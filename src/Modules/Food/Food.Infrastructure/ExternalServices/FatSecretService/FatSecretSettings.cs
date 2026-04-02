namespace Food.Infrastructure.ExternalServices.FatSecretService
{
    public sealed record FatSecretSettings {
        public string BaseUrl { get; init; }
        public string TokenUrl { get; init; }
        public string GrantType { get; init; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string Scopes { get; init; }

        public FatSecretSettings(){

        }
    }
        
}