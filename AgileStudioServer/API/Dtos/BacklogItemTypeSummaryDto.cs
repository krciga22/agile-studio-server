using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemTypeSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemTypeSummaryDto(BacklogItemType backlogItemType)
        {
            ID = backlogItemType.ID;
            Title = backlogItemType.Title;
            Description = backlogItemType.Description;
            CreatedOn = backlogItemType.CreatedOn;
        }
    }
}
