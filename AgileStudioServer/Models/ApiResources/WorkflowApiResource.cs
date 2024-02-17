using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class WorkflowApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public WorkflowApiResource(Workflow workflow)
        {
            ID = workflow.ID;
            Title = workflow.Title;
            Description = workflow.Description;
            CreatedOn = workflow.CreatedOn;
        }
    }
}
