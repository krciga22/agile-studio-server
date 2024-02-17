using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Models.Entities
{
    public class WorkflowState
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [Required]
        public Workflow Workflow { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public WorkflowState(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}