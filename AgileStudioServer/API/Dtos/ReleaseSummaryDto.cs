using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class ReleaseSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ReleaseSummaryDto(Release release)
        {
            ID = release.ID;
            Title = release.Title;
        }
    }
}
