using AgileStudioServer.Data.Entities;

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

        public BacklogItemDto(BacklogItem backlogItem)
        {
            ID = backlogItem.ID;
            Title = backlogItem.Title;
            Description = backlogItem.Description;
            CreatedOn = backlogItem.CreatedOn;
            CreatedBy = backlogItem.CreatedBy is null ? null : new UserSummaryDto(backlogItem.CreatedBy);
            Project = new ProjectSummaryDto(backlogItem.Project);
            BacklogItemType = new BacklogItemTypeSummaryDto(backlogItem.BacklogItemType);
            WorkflowState = new WorkflowStateSummaryDto(backlogItem.WorkflowState);

            if (backlogItem.Sprint != null)
            {
                Sprint = new SprintSummaryDto(backlogItem.Sprint);
            }

            if (backlogItem.Release != null)
            {
                Release = new ReleaseSummaryDto(backlogItem.Release);
            }
        }
    }
}
