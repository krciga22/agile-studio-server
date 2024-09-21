using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.ApiResources
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
