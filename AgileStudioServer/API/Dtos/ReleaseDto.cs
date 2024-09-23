using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class ReleaseDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ProjectSummaryDto Project { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ReleaseDto(Release release)
        {
            ID = release.ID;
            Title = release.Title;
            Project = new ProjectSummaryDto(release.Project);
            Description = release.Description;
            CreatedOn = release.CreatedOn;
            CreatedBy = release.CreatedBy is null ? null : new UserSummaryDto(release.CreatedBy);
            StartDate = release.StartDate;
            EndDate = release.EndDate;
        }
    }
}
