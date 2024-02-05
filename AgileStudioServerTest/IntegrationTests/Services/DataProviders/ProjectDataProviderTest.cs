using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Services.DataProviders
{
    public class ProjectDataProviderTest : AbstractDataProviderTest
    {
        private readonly ProjectDataProvider _ProjectDataProvider;

        public ProjectDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            ProjectDataProvider projectDataProvider) : base(dbContext, fixtures)
        {
            _ProjectDataProvider = projectDataProvider;
        }

        [Fact]
        public void CreateProject_WithProjectPostDto_ReturnsProjectApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var projectPostDto = new ProjectPostDto("Test Project", backlogItemTypeSchema.ID);

            var projectApiResource = _ProjectDataProvider.Create(projectPostDto);

            Assert.IsType<ProjectApiResource>(projectApiResource);
        }

        [Fact]
        public void GetProject_ById_ReturnsProjectApiResource()
        {
            var project = _Fixtures.CreateProject();

            var projectApiResource = _ProjectDataProvider.Get(project.ID);

            Assert.IsType<ProjectApiResource>(projectApiResource);
        }

        [Fact]
        public void GetProjects_WithNoArguments_ReturnsProjectApiResources()
        {
            var projects = new List<Project>
            {
                _Fixtures.CreateProject("Test Project 1"),
                _Fixtures.CreateProject("Test Project 2")
            };

            List<ProjectApiResource> projectApiResources = _ProjectDataProvider.List();

            Assert.Equal(projects.Count, projectApiResources.Count);
        }

        [Fact]
        public void UpdateProject_WithValidProjectPatchDto_ReturnsProjectApiResource()
        {
            var project = _Fixtures.CreateProject();
            var title = $"{project.Title} Updated";
            var projectPatchDto = new ProjectPatchDto(title);

            var projectApiResource = _ProjectDataProvider.Update(project.ID, projectPatchDto);

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(projectPatchDto.Title, projectApiResource.Title);
        }

        [Fact]
        public void DeleteProject_WithValidId_ReturnsTrue()
        {
            var project = _Fixtures.CreateProject();

            bool result = _ProjectDataProvider.Delete(project.ID);

            Assert.True(result);
        }
    }
}
