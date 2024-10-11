
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

        public ReleaseDto(
            int id,
            string title,
            ProjectSummaryDto project,
            DateTime createdOn)
        {
            ID = id;
            Title = title;
            Project = project;
            CreatedOn = createdOn;
        }
    }
}
