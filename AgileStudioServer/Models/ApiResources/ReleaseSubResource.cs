using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class ReleaseSubResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ReleaseSubResource(Release release)
        {
            ID = release.ID;
            Title = release.Title;
        }
    }
}
