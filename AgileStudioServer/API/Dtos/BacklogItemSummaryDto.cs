using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public BacklogItemSummaryDto(BacklogItem backlogItem)
        {
            ID = backlogItem.ID;
            Title = backlogItem.Title;
        }
    }
}
