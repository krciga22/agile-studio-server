using AgileStudioServer.API.Dtos;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class SprintRepositoryTest : AbstractServiceTest
    {
        private readonly SprintService _sprintService;

        public SprintRepositoryTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            SprintService sprintService) : base(dbContext, fixtures)
        {
            _sprintService = sprintService;
        }

        [Fact]
        public void Create_ReturnsSprint()
        {
            int nextSprintNumber = _sprintService.GetNextSprintNumber();
            Sprint sprint = new(nextSprintNumber);
            sprint.Project = _Fixtures.CreateProject();

            sprint = _sprintService.Create(sprint);

            Assert.NotNull(sprint);
            Assert.True(sprint.ID > 0);
        }

        [Fact]
        public void Get_ReturnsSprint()
        {
            var sprint = _Fixtures.CreateSprint();

            var returnedSprint = _sprintService.Get(sprint.ID);

            Assert.NotNull(returnedSprint);
            Assert.Equal(sprint.ID, returnedSprint.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllSprints()
        {
            var project = _Fixtures.CreateProject();
            var nextSprintNumber = _sprintService.GetNextSprintNumber();
            var sprints = new List<Sprint>
            {
                _Fixtures.CreateSprint(nextSprintNumber + 1, project),
                _Fixtures.CreateSprint(nextSprintNumber + 2,project)
            };

            List<Sprint> returnedSprints = _sprintService.GetByProjectId(project.ID);

            Assert.Equal(sprints.Count, returnedSprints.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedSprint()
        {
            var sprint = _Fixtures.CreateSprint();
            var nextSprintNumber = _sprintService.GetNextSprintNumber();

            sprint.SprintNumber = nextSprintNumber;
            sprint = _sprintService.Update(sprint);

            Assert.NotNull(sprint);
            Assert.Equal(nextSprintNumber, sprint.SprintNumber);
        }

        [Fact]
        public void Delete_DeletesSprint()
        {
            var sprint = _Fixtures.CreateSprint();

            _sprintService.Delete(sprint);

            sprint = _sprintService.Get(sprint.ID);
            Assert.Null(sprint);
        }
    }
}
