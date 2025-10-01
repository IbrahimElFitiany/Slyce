namespace WebAPI.RequestModels
{
    public class KeycloakEventRequest
    {
        public required string EventId { get; set; }
        public long Time { get; set; }
        public required string Type { get; set; }
        public required string ClientId { get; set; }
        public required string UserId { get; set; }
        public string? Fname  { get; set; }
        public string? Lname { get; set; }
        public string? Email { get; set; }
        public required string Role { get; set; }
    }
}