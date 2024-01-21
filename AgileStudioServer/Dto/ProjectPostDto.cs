using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Dto
{
    public class ProjectPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public ProjectPostDto(string title)
        {
            Title = title;
        }
    }
}
