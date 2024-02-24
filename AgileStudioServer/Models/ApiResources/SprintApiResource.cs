using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class SprintApiResource
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public ProjectSubResource Project { get; set; }

        public string? Description { get; set; } = null;

        public DateTime CreatedOn { get; set; }

        public UserSubResource? CreatedBy { get; set; }

        public DateTime? StartDate { get; set; } = null;
        
        public DateTime? EndDate { get; set; } = null;

        public SprintApiResource(Sprint sprint)
        {
            ID = sprint.ID;
            SprintNumber = sprint.SprintNumber;
            Project = new ProjectSubResource(sprint.Project);
            Description = sprint.Description;
            CreatedOn = sprint.CreatedOn;
            CreatedBy = sprint.CreatedBy is null ? null : new UserSubResource(sprint.CreatedBy);
            StartDate = sprint.StartDate;
            EndDate = sprint.EndDate;
        }
    }
}
