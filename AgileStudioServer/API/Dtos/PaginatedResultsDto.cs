
using AgileStudioServer.Core.Pagination;

namespace AgileStudioServer.API.Dtos
{
    public class PaginatedResultsDto<T, J> 
        where T : class 
        where J : class
    {
        public List<T> Items { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int? PrevPage { get; set; }

        public int? NextPage { get; set; }

        public PaginatedResultsDto(List<T> items, PaginationResults<J> paginationResults)
        {
            Items = items;
            Total = paginationResults.Total;
            Page = paginationResults.Page;
            TotalPages = paginationResults.TotalPages;
            PrevPage = paginationResults.PrevPage;
            NextPage = paginationResults.NextPage;
        }
    }
}
