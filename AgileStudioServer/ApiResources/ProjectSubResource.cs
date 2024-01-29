using AgileStudioServer.Models;

namespace AgileStudioServer.ApiResources
{
    public class ProjectSubResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ProjectSubResource(Project project)
        {
            ID = project.ID;
            Title = project.Title;
        }
    }
}
