
namespace AgileStudioServer.API.DtosNew
{
    public class BacklogItemTypeDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaSummaryDto BacklogItemTypeSchema { get; set; }

        public WorkflowSummaryDto Workflow { get; set; }

        public BacklogItemTypeDto(
            int id,
            string title,
            DateTime createdOn,
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchema,
            WorkflowSummaryDto workflow)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
            BacklogItemTypeSchema = backlogItemTypeSchema;
            Workflow = workflow;
        }
    }
}
