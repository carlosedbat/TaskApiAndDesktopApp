using DataSystem.Shared.DTOs.Pagination;

namespace DataSystem.Shared.DTOs.Response
{
    public class ServiceListResponseDTO<T>
    {
        public ServiceResponseDTO<List<T>> ServiceResponseDTO { get; set; } = new ServiceResponseDTO<List<T>>();

        public PaginationDTO PaginationDTO { get; set; } = new PaginationDTO(10,0,1);

    }
}
