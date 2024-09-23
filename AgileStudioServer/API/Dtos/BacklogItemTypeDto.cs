using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
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

        public BacklogItemTypeDto(BacklogItemType backlogItemType)
        {
            ID = backlogItemType.ID;
            Title = backlogItemType.Title;
            Description = backlogItemType.Description;
            CreatedOn = backlogItemType.CreatedOn;
            CreatedBy = backlogItemType.CreatedBy is null ? null : new UserSummaryDto(backlogItemType.CreatedBy);
            BacklogItemTypeSchema = new BacklogItemTypeSchemaSummaryDto(backlogItemType.BacklogItemTypeSchema);
            Workflow = new WorkflowSummaryDto(backlogItemType.Workflow);
        }
    }
}
