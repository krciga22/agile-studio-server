
namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemTypeSchemaSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public BacklogItemTypeSchemaSummaryDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
