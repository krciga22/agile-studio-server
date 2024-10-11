using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Dtos
{
    public class WorkflowPatchDto
    {
        [Required]
        public int ID;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowPatchDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
