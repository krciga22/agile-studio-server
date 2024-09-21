using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Dtos
{
    public class WorkflowStatePostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int WorkflowId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowStatePostDto(string title, int workflowId)
        {
            Title = title;
            WorkflowId = workflowId;
        }
    }
}
