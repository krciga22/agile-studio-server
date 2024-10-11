
namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public ProjectSummaryDto Project { get; set; }

        public BacklogItemTypeSummaryDto BacklogItemType { get; set; }

        public WorkflowStateSummaryDto WorkflowState { get; set; }

        public SprintSummaryDto? Sprint { get; set; } = null;

        public ReleaseSummaryDto? Release { get; set; } = null;

        public BacklogItemDto(
            int id, 
            string title, 
            DateTime createdOn, 
            ProjectSummaryDto project, 
            BacklogItemTypeSummaryDto backlogItemType,
            WorkflowStateSummaryDto workflowState)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
            Project = project;
            BacklogItemType = backlogItemType;
            WorkflowState = workflowState;
        }
    }
}
