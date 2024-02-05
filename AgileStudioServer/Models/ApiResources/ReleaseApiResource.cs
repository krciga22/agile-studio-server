using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class ReleaseApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ProjectSubResource Project { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ReleaseApiResource(Release release)
        {
            ID = release.ID;
            Title = release.Title;
            Project = new ProjectSubResource(release.Project);
            Description = release.Description;
            CreatedOn = release.CreatedOn;
            StartDate = release.StartDate;
            EndDate = release.EndDate;
        }
    }
}
