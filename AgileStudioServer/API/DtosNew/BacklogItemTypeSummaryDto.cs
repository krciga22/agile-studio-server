
namespace AgileStudioServer.API.DtosNew
{
    public class BacklogItemTypeSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemTypeSummaryDto(
            int id,
            string title,
            DateTime createdOn)
        {
            ID = id;
            Title = title;
            CreatedOn = createdOn;
        }
    }
}
