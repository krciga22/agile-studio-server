using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Dtos
{
    public class WorkflowStatePatchDto
    {
        [Required]
        public int ID;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowStatePatchDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
