using AgileStudioServer.ApiResources;
using AgileStudioServer.Models;

namespace AgileStudioServer.DataProviders
{
    public class ProjectDataProvider
    {
        private DBContext _DBContext;

        public ProjectDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public List<ProjectApiResource> GetProjects()
        {
            List<Project> projects = _DBContext.Projects.ToList();
            List<ProjectApiResource> projectApiResources = new();
            projects.ForEach(project => {
                ProjectApiResource projectApiResource = new(project);
            });
            return projectApiResources;
        }
    }
}
