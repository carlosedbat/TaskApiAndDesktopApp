using DataSystem.Shared.DTOs.Pagination;

namespace DataSystem.FrontendWpf.Models.Api.DTO
{
    public class ApiListResponseWrapperDTO<T>
    {
        public ApiResponseListDTO<T> ServiceResponseDTO { get; set; } = new();
        //TODO [REFACT] Mover numeros para constantes
        public PaginationDTO PaginationDTO { get; set; } = new(10,0,1);
    }
}
