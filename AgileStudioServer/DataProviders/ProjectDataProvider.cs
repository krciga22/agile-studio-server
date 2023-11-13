using AgileStudioServer.ApiResources;
using AgileStudioServer.Dto;
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

        public virtual List<ProjectApiResource> GetProjects()
        {
            List<Project> projects = _DBContext.Projects.ToList();

            List<ProjectApiResource> projectApiResources = new();
            projects.ForEach(project => {
                projectApiResources.Add(
                    new ProjectApiResource(project)
                );
            });

            return projectApiResources;
        }

        public virtual ProjectApiResource? GetProject(int id)
        {
            Project? project = _DBContext.Projects.Find(id);
            return project != null ? new ProjectApiResource(project) : null;
        }

        public virtual ProjectApiResource CreateProject(ProjectPostDto projectPostDto)
        {
            var project = new Project(projectPostDto.Title) {
                Description = projectPostDto.Description
            };

            _DBContext.Add(project);
            _DBContext.SaveChanges();

            return new ProjectApiResource(project);
        }
    }
}
