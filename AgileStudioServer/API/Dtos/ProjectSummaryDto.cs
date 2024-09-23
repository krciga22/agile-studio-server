using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class ProjectSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ProjectSummaryDto(Project project)
        {
            ID = project.ID;
            Title = project.Title;
        }
    }
}
