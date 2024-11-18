namespace AgileStudioServer.Core.Pagination
{
    public class PaginationDetails
    {
        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public PaginationDetails(int page = Constants.DefaultPage, int itemsPerPage = Constants.MaxItemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
