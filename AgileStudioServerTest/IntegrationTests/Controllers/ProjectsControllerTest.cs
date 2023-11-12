using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.Controllers;
using AgileStudioServer.Dto;
using AgileStudioServerTest.IntegrationTests.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class ProjectsControllerTest : AbstractControllerTest
    {
        private readonly ProjectsController _ProjectsController;

        public ProjectsControllerTest(DBContext dbContext, ProjectsController projectsController) : base(dbContext) {
            _ProjectsController = projectsController;
        }

        [Fact]
        public void Post_ProjectPostDto_ReturnsProjectApiResource()
        {
            var projectPostDto = new ProjectPostDto("Test Project");
            var projectApiResource = _ProjectsController.Post(projectPostDto);
            Assert.IsType<ProjectApiResource>(projectApiResource);
        }
    }
}
