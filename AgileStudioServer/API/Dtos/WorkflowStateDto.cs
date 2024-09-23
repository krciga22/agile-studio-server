using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class WorkflowStateDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSummaryDto Workflow { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public WorkflowStateDto(WorkflowState workflowState)
        {
            ID = workflowState.ID;
            Title = workflowState.Title;
            Description = workflowState.Description;
            CreatedOn = workflowState.CreatedOn;
            CreatedBy = workflowState.CreatedBy is null ? null : new UserSummaryDto(workflowState.CreatedBy);
            Workflow = new WorkflowSummaryDto(workflowState.Workflow);
        }
    }
}
