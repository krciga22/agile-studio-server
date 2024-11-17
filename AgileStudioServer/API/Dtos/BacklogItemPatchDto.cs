using AgileStudioServer.API.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Dtos
{
    [ValidSprintForBacklogItem]
    [ValidReleaseForBacklogItem]
    [ValidWorkflowStateForBacklogItem]
    [ValidParentBacklogItemForBacklogItem]
    public class BacklogItemPatchDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public int WorkflowStateId { get; set; }

        public int? SprintId { get; set; } = null;

        public int? ReleaseId { get; set; } = null;

        public int? ParentBacklogItemId { get; set; } = null;

        public BacklogItemPatchDto(int id, string title, int workflowStateId)
        {
            ID = id;
            Title = title;
            WorkflowStateId = workflowStateId;
        }
    }
}
