using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemTypeSchemaSubResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public BacklogItemTypeSchemaSubResource(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            ID = backlogItemTypeSchema.ID;
            Title = backlogItemTypeSchema.Title;
        }
    }
}
