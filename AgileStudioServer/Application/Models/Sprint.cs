using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Application.Models
{
    public class Sprint
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        [Required]
        public Project Project { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public User? CreatedBy { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Sprint(int sprintNumber)
        {
            SprintNumber = sprintNumber;
            CreatedOn = DateTime.Now;
        }
    }
}