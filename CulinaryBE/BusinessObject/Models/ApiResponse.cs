namespace BusinessObject.Models
{
    public class ApiResponse
    {
        public ApiResponse() { }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public string? Errors { get; set; }
        public object? Result { get; set; }
    }
}
