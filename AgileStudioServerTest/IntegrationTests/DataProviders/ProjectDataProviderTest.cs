using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;

namespace AgileStudioServerTest.IntegrationTests.DataProviders
{
    public class ProjectDataProviderTest : AbstractDataProviderTest
    {
        private readonly ProjectDataProvider _ProjectDataProvider;

        public ProjectDataProviderTest(DBContext dbContext, ProjectDataProvider projectDataProvider) : base(dbContext)
        {
            _ProjectDataProvider = projectDataProvider;
        }

        [Fact]
        public void CreateProject_WithProjectPostDto_ReturnsProjectApiResource()
        {
            var projectPostDto = new ProjectPostDto() {
                Title = "Test Project"
            };

            var projectApiResource = _ProjectDataProvider.CreateProject(projectPostDto);

            Assert.IsType<ProjectApiResource>(projectApiResource);
        }

        [Fact]
        public void GetProject_ById_ReturnsProjectApiResource()
        {
            var project = CreateProject();

            var projectApiResource = _ProjectDataProvider.GetProject(project.ID);

            Assert.IsType<ProjectApiResource>(projectApiResource);
        }

        [Fact]
        public void GetProjects_WithNoArguments_ReturnsProjectApiResources()
        {
            var projects = new List<Project>
            {
                CreateProject("Test Project 1"),
                CreateProject("Test Project 2")
            };

            List<ProjectApiResource> projectApiResources = _ProjectDataProvider.GetProjects();

            Assert.Equal(projects.Count, projectApiResources.Count);
        }

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            _DBContext.Projects.Add(project);
            _DBContext.SaveChanges();
            return project;
        }
    }
}
