using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
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

        public SprintDto(Sprint sprint)
        {
            ID = sprint.ID;
            SprintNumber = sprint.SprintNumber;
            Project = new ProjectSummaryDto(sprint.Project);
            Description = sprint.Description;
            CreatedOn = sprint.CreatedOn;
            CreatedBy = sprint.CreatedBy is null ? null : new UserSummaryDto(sprint.CreatedBy);
            StartDate = sprint.StartDate;
            EndDate = sprint.EndDate;
        }
    }
}
