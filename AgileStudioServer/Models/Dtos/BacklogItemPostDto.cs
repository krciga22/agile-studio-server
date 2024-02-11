using AgileStudioServer.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Models.Dtos
{
    [ValidBacklogItemTypeForBacklogItemPostDto]
    [ValidSprintForBacklogItem]
    [ValidReleaseForBacklogItem]
    public class BacklogItemPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int BacklogItemTypeId { get; set; }

        public int? SprintId { get; set; } = null;

        public int? ReleaseId { get; set; } = null;

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemPostDto(string title, int projectId, int backlogItemTypeId, int? sprintId = null, int? releaseId = null)
        {
            Title = title;
            ProjectId = projectId;
            BacklogItemTypeId = backlogItemTypeId;
            SprintId = sprintId;
            ReleaseId = releaseId;
        }
    }
}
