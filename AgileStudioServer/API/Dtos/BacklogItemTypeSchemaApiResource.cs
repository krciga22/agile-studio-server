using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemTypeSchemaApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSubResource? CreatedBy { get; set; }

        public BacklogItemTypeSchemaApiResource(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            ID = backlogItemTypeSchema.ID;
            Title = backlogItemTypeSchema.Title;
            Description = backlogItemTypeSchema.Description;
            CreatedOn = backlogItemTypeSchema.CreatedOn;
            CreatedBy = backlogItemTypeSchema.CreatedBy is null ? null : new UserSubResource(backlogItemTypeSchema.CreatedBy);
        }
    }
}
