
namespace AgileStudioServer.Application.Models
{
    public class Sprint
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public int ProjectID { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Sprint(int sprintNumber, int projectId)
        {
            SprintNumber = sprintNumber;
            CreatedOn = DateTime.Now;
            ProjectID = projectId;
        }
    }
}