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

        public virtual List<ProjectApiResource> List()
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

        public virtual ProjectApiResource? Get(int id)
        {
            Project? project = _DBContext.Projects.Find(id);
            return project != null ? new ProjectApiResource(project) : null;
        }

        public virtual ProjectApiResource Create(ProjectPostDto projectPostDto)
        {
            var project = new Project(projectPostDto.Title) {
                Description = projectPostDto.Description
            };

            _DBContext.Add(project);
            _DBContext.SaveChanges();

            return new ProjectApiResource(project);
        }

        public virtual ProjectApiResource? Update(int id, ProjectPatchDto projectPatchDto)
        {
            var project = _DBContext.Projects.Find(id);
            if(project is null){
                return null;
            }

            project.Title = projectPatchDto.Title;
            project.Description = projectPatchDto.Description;
            _DBContext.SaveChanges();

            return new ProjectApiResource(project);
        }

        public virtual bool Delete(int id)
        {
            var project = _DBContext.Projects.Find(id);
            if(project is null){
                return false;
            }

            _DBContext.Projects.Remove(project);
            _DBContext.SaveChanges();
            return true;
        }
    }
}
