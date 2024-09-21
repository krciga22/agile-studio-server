using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.ApiResources
{
    public class WorkflowApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSubResource? CreatedBy { get; set; }

        public WorkflowApiResource(Workflow workflow)
        {
            ID = workflow.ID;
            Title = workflow.Title;
            Description = workflow.Description;
            CreatedOn = workflow.CreatedOn;
            CreatedBy = workflow.CreatedBy is null ? null : new UserSubResource(workflow.CreatedBy);
        }
    }
}
