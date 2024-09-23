using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class WorkflowSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSummaryDto(Workflow workflow)
        {
            ID = workflow.ID;
            Title = workflow.Title;
        }
    }
}
