using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemTypeSchemaSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public BacklogItemTypeSchemaSummaryDto(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            ID = backlogItemTypeSchema.ID;
            Title = backlogItemTypeSchema.Title;
        }
    }
}
