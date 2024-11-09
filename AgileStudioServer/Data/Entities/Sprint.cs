
namespace AgileStudioServer.Data.Entities
{
    public class Sprint
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Sprint(int sprintNumber, int projectID)
        {
            SprintNumber = sprintNumber;
            CreatedOn = DateTime.Now;
            ProjectID = projectID;
        }
    }
}