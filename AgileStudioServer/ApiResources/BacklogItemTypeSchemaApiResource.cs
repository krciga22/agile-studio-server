using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.ApiResources
{
    public class BacklogItemTypeSchemaApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemTypeSchemaApiResource(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            ID = backlogItemTypeSchema.ID;
            Title = backlogItemTypeSchema.Title;
            Description = backlogItemTypeSchema.Description;
            CreatedOn = backlogItemTypeSchema.CreatedOn;
        }
    }
}
