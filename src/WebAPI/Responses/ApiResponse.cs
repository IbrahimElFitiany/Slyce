namespace WebAPI.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public required string Message { get; init; }
        public int Status { get; init; }
        public T? Data { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;

        private ApiResponse() { }
        public static ApiResponse<T> SuccessResponse(T data, string message, int status = 200)
            => new() { Success = true, Data = data, Message = message, Status = status };

        public static ApiResponse<T> ErrorResponse(string message, int status = 500)
            => new() { Success = false, Message = message, Status = status };
    }
}
