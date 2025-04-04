namespace DataSystem.FrontendWpf.Models.Api.DTO
{
    public class ApiResponseDTO<T>
    {
        public int TotalPages { get; set; }
        public T GenericData { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
