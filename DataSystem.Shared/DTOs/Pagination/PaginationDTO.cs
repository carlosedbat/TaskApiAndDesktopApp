namespace DataSystem.Shared.DTOs.Pagination
{
    public class PaginationDTO
    {
        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public bool HasNext => this.CurrentPage < this.TotalPages;

        public bool HasPrevious => this.CurrentPage > 1;

        public PaginationDTO(int pageSize, int totalCount, int currentPage)
        {
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage == 0 ? 1 : currentPage;
            this.TotalPages = this.DivideAndRoundUp(totalCount, pageSize);
        }

        private int DivideAndRoundUp(double dividend, double divisor)
        {
            double resultOfDivision = dividend / divisor;

            int resultRounded = (int)Math.Ceiling(resultOfDivision);

            return resultRounded;
        }
    }
}
