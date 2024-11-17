using AgileStudioServer.API.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Dtos
{
    [ValidBacklogItemTypeForBacklogItemPostDto]
    [ValidSprintForBacklogItem]
    [ValidReleaseForBacklogItem]
    [ValidWorkflowStateForBacklogItem]
    public class BacklogItemPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int BacklogItemTypeId { get; set; }

        [Required]
        public int WorkflowStateId { get; set; }

        public int? SprintId { get; set; } = null;

        public int? ReleaseId { get; set; } = null;

        public int? ParentBacklogItemId { get; set; } = null;

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemPostDto(string title, int projectId, int backlogItemTypeId, int workflowStateId)
        {
            Title = title;
            ProjectId = projectId;
            BacklogItemTypeId = backlogItemTypeId;
            WorkflowStateId = workflowStateId;
        }
    }
}
