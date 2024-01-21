using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Dto
{
    public class BacklogItemTypeSchemaPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemTypeSchemaPostDto(string title, int projectId)
        {
            Title = title;
            ProjectId = projectId;
        }
    }
}
