namespace BusinessObject.Models
{
    public class ApiResponse
    {
        public ApiResponse() { }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public string? Error { get; set; }
        public object? Result { get; set; }
    }
}
