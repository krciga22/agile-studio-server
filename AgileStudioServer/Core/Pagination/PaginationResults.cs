namespace AgileStudioServer.Core.Pagination
{
    public class PaginationResults<T> where T : class
    {
        public List<T> Items { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalPages { get; set; } = 1;

        public int? PrevPage { get; set; } = null;

        public int? NextPage { get; set; } = null;

        public PaginationResults(List<T> items, int total, int page, int itemsPerPage)
        {
            Items = items;
            Total = total;
            Page = page;
            ItemsPerPage = itemsPerPage;

            TotalPages = (int) Math.Ceiling((double) Total / ItemsPerPage);

            if(Page + 1 <= TotalPages)
            {
                NextPage = Page + 1;
            }

            if(Page - 1 > 0)
            {
                PrevPage = Page - 1;
            }
        }
    }
}
