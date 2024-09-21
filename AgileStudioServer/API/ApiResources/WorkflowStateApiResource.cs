using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.ApiResources
{
    public class WorkflowStateApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSubResource Workflow { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSubResource? CreatedBy { get; set; }

        public WorkflowStateApiResource(WorkflowState workflowState)
        {
            ID = workflowState.ID;
            Title = workflowState.Title;
            Description = workflowState.Description;
            CreatedOn = workflowState.CreatedOn;
            CreatedBy = workflowState.CreatedBy is null ? null : new UserSubResource(workflowState.CreatedBy);
            Workflow = new WorkflowSubResource(workflowState.Workflow);
        }
    }
}
