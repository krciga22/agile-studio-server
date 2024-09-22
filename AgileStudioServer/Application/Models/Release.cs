using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Application.Models
{
    public class Release
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [Required]
        public Project Project { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public User? CreatedBy { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Release(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}