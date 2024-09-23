using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class WorkflowStateSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowStateSummaryDto(WorkflowState workflowState)
        {
            ID = workflowState.ID;
            Title = workflowState.Title;
        }
    }
}
