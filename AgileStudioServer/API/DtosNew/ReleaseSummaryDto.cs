
namespace AgileStudioServer.API.Dtos
{
    public class ReleaseSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ReleaseSummaryDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
