using System.ComponentModel.DataAnnotations;

namespace DataSystem.Shared.DTOs.Pagination
{
    /// <summary>
    ///  Represents the parameters for pagination in API requests.
    /// </summary>
    public class PaginationParamsDTO
    {
        /// <summary>
        /// The current page index. Defaults to 1.
        /// </summary>
        /// <example>1</example>
        [Range(1, int.MaxValue, ErrorMessage = "O index da pagina deve ser 1 ou maior.")]
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;

        /// <summary>
        /// The number of items per page. Defaults to 10. Must be 1 or greater.
        /// </summary>
        /// <example>10</example>
        [Range(1, int.MaxValue, ErrorMessage = "O tamanho de itens por pagina deve ser 1 ou maior.")]
        public int PageSize
        {
            get => this._pageSize;
            set => this._pageSize = value < 1 ? 1 : value;
        }
    }
}
