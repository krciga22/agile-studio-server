using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Dto
{
    public class BacklogItemPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int BacklogItemTypeId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemPostDto(string title, int projectId, int backlogItemTypeId)
        {
            Title = title;
            ProjectId = projectId;
            BacklogItemTypeId = backlogItemTypeId;
        }
    }
}
