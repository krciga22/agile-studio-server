using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;

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
        public void Create_Project_ReturnsProjectApiResource()
        {
            var projectPostDto = new ProjectPostDto("Test Project");

            var projectApiResource = _ProjectDataProvider.CreateProject(projectPostDto);

            Assert.IsType<ProjectApiResource>(projectApiResource);
        }
    }
}
