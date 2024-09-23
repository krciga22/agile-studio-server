using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public class SprintDataProviderTest : AbstractDataProviderTest
    {
        private readonly SprintDataProvider _DataProvider;

        public SprintDataProviderTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            SprintDataProvider sprintDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = sprintDataProvider;
        }

        [Fact]
        public void CreateSprint_WithSprintPostDto_ReturnsSprintApiResource()
        {
            var project = _Fixtures.CreateProject();
            var sprintPostDto = new SprintPostDto(project.ID);

            var sprintApiResource = _DataProvider.Create(sprintPostDto);

            Assert.IsType<SprintApiResource>(sprintApiResource);
        }

        [Fact]
        public void GetSprint_ById_ReturnsSprintApiResource()
        {
            var sprint = _Fixtures.CreateSprint();

            var sprintApiResource = _DataProvider.Get(sprint.ID);

            Assert.IsType<SprintApiResource>(sprintApiResource);
        }

        [Fact]
        public void GetSprints_ByProjectId_ReturnsSprintApiResources()
        {
            var project = _Fixtures.CreateProject();

            var sprints = new List<Sprint>
            {
                _Fixtures.CreateSprint(1, project),
                _Fixtures.CreateSprint(2, project)
            };

            List<SprintApiResource> sprintApiResources = _DataProvider.ListForProjectId(project.ID);

            Assert.Equal(sprints.Count, sprintApiResources.Count);
        }

        [Fact]
        public void UpdateSprint_WithValidSprintPatchDto_ReturnsSprintApiResource()
        {
            var sprint = _Fixtures.CreateSprint();
            var sprintPatchDto = new SprintPatchDto
            {
                Description = $"Sprint {sprint.SprintNumber} Description Updated"
            };

            var sprintApiResource = _DataProvider.Update(sprint.ID, sprintPatchDto);

            Assert.IsType<SprintApiResource>(sprintApiResource);
            Assert.Equal(sprintPatchDto.Description, sprintApiResource.Description);
        }

        [Fact]
        public void DeleteSprint_WithValidId_ReturnsTrue()
        {
            var sprint = _Fixtures.CreateSprint();

            _DataProvider.Delete(sprint.ID);

            var result = _DataProvider.Get(sprint.ID);
            Assert.Null(result);
        }
    }
}
