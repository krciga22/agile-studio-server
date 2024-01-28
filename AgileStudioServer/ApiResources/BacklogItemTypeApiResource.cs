using AgileStudioServer.Models;

namespace AgileStudioServer.ApiResources
{
    public class BacklogItemTypeApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemTypeSchemaApiResource BacklogItemTypeSchema { get; set; }

        public BacklogItemTypeApiResource(BacklogItemType backlogItemType)
        {
            ID = backlogItemType.ID;
            Title = backlogItemType.Title;
            Description = backlogItemType.Description;
            CreatedOn = backlogItemType.CreatedOn;
            BacklogItemTypeSchema = new BacklogItemTypeSchemaApiResource(backlogItemType.BacklogItemTypeSchema);
        }
    }
}
