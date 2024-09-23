
namespace AgileStudioServer.API.DtosNew
{
    public class SprintDto
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public ProjectSummaryDto Project { get; set; }

        public string? Description { get; set; } = null;

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;

        public SprintDto(
            int id,
            int sprintNumber,
            ProjectSummaryDto project,
            DateTime createdOn)
        {
            ID = id;
            SprintNumber = sprintNumber;
            Project = project;
            CreatedOn = createdOn;
        }
    }
}
