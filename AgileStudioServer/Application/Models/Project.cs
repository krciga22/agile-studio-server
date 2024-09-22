using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Application.Models
{
    public class Project
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public User? CreatedBy { get; set; } = null!;

        [Required]
        public BacklogItemTypeSchema BacklogItemTypeSchema { get; set; } = null!;

        public Project(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}