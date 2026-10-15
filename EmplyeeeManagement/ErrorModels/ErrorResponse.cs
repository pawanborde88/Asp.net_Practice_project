namespace EmployeeManagement.ErrorModels
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public DateTime TimeStamp { get; set; }

        public string TraceId { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;
    }
}