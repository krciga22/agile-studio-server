
namespace AgileStudioServer.API.Dtos
{
    public class ProjectSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ProjectSummaryDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
