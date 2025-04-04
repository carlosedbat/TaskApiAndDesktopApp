namespace DataSystem.FrontendWpf.Models.Api.DTO
{
    public class ApiResponseListDTO<T>
    {
        public int TotalPages { get; set; }
        public List<T> GenericData { get; set; } = new List<T>();
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
