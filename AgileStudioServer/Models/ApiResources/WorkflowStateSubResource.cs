using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class WorkflowStateSubResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowStateSubResource(WorkflowState workflowState)
        {
            ID = workflowState.ID;
            Title = workflowState.Title;
        }
    }
}
