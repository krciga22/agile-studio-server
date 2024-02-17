using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class WorkflowStateApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSubResource Workflow { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public WorkflowStateApiResource(WorkflowState workflowState)
        {
            ID = workflowState.ID;
            Title = workflowState.Title;
            Description = workflowState.Description;
            CreatedOn = workflowState.CreatedOn;
            Workflow = new WorkflowSubResource(workflowState.Workflow);
        }
    }
}
