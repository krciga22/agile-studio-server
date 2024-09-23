using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemTypeSchemaDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaDto(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            ID = backlogItemTypeSchema.ID;
            Title = backlogItemTypeSchema.Title;
            Description = backlogItemTypeSchema.Description;
            CreatedOn = backlogItemTypeSchema.CreatedOn;
            CreatedBy = backlogItemTypeSchema.CreatedBy is null ? null : new UserSummaryDto(backlogItemTypeSchema.CreatedBy);
        }
    }
}
