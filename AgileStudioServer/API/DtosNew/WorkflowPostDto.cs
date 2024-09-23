using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.DtosNew
{
    public class WorkflowPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowPostDto(string title)
        {
            Title = title;
        }
    }
}
