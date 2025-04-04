namespace DataSystem.Shared.DTOs.Response
{
    public class ListResponseDTO<T>
    {
        public int TotalPages { get; set; }

        public List<T> Data { get; set; }

        public int? TotalCount { get; set; }
    }
}
