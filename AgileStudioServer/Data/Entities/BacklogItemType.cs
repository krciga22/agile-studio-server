namespace AgileStudioServer.Data.Entities
{
    public class BacklogItemType
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public int BacklogItemTypeSchemaID { get; set; }

        public BacklogItemTypeSchema BacklogItemTypeSchema { get; set; } = null!;

        public int WorkflowID { get; set; }

        public Workflow Workflow { get; set; } = null!;

        public BacklogItemType(string title, int backlogItemTypeSchemaID, int workflowID)
        {
            Title = title;
            CreatedOn = DateTime.Now;
            BacklogItemTypeSchemaID = backlogItemTypeSchemaID;
            WorkflowID = workflowID;
        }
    }
}