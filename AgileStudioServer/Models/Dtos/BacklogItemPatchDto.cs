using AgileStudioServer.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Models.Dtos
{
    [ValidSprintForBacklogItem]
    public class BacklogItemPatchDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public int? SprintId { get; set; } = null;

        public BacklogItemPatchDto(string title, int? sprintId = null)
        {
            Title = title;
            SprintId = sprintId;
        }
    }
}
