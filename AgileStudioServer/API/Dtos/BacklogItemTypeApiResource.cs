using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemTypeApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSubResource? CreatedBy { get; set; }

        public BacklogItemTypeSchemaSubResource BacklogItemTypeSchema { get; set; }

        public WorkflowSubResource Workflow { get; set; }

        public BacklogItemTypeApiResource(BacklogItemType backlogItemType)
        {
            ID = backlogItemType.ID;
            Title = backlogItemType.Title;
            Description = backlogItemType.Description;
            CreatedOn = backlogItemType.CreatedOn;
            CreatedBy = backlogItemType.CreatedBy is null ? null : new UserSubResource(backlogItemType.CreatedBy);
            BacklogItemTypeSchema = new BacklogItemTypeSchemaSubResource(backlogItemType.BacklogItemTypeSchema);
            Workflow = new WorkflowSubResource(backlogItemType.Workflow);
        }
    }
}
