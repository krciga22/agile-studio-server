using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Dtos
{
    public class WorkflowPatchDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowPatchDto(string title)
        {
            Title = title;
        }
    }
}
