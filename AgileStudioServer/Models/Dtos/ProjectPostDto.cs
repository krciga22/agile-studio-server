using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Models.Dtos
{
    public class ProjectPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int BacklogItemTypeSchemaId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public ProjectPostDto(string title, int backlogItemTypeSchemaId)
        {
            Title = title;
            BacklogItemTypeSchemaId = backlogItemTypeSchemaId;
        }
    }
}
