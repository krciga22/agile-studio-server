using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class BacklogItemServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemService _backlogItemService;

        public BacklogItemServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            BacklogItemService backlogItemService) : base(dbContext, fixtures)
        {
            _backlogItemService = backlogItemService;
        }

        [Fact]
        public void Create_ReturnsBacklogItem()
        {
            BacklogItem backlogItem = new("Test BacklogItem");
            backlogItem.Project = _Fixtures.CreateProject();
            backlogItem.BacklogItemType = _Fixtures.CreateBacklogItemType();
            backlogItem.WorkflowState = _Fixtures.CreateWorkflowState();

            backlogItem = _backlogItemService.Create(backlogItem);

            Assert.NotNull(backlogItem);
            Assert.True(backlogItem.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItem()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            var returnedBacklogItem = _backlogItemService.Get(backlogItem.ID);

            Assert.NotNull(returnedBacklogItem);
            Assert.Equal(backlogItem.ID, returnedBacklogItem.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllBacklogItems()
        {
            var project = _Fixtures.CreateProject();
            var backlogItems = new List<BacklogItem>
            {
                _Fixtures.CreateBacklogItem("Test BacklogItem 1", project: project),
                _Fixtures.CreateBacklogItem("Test BacklogItem 2", project: project)
            };

            List<BacklogItem> returnedBacklogItems = _backlogItemService
                .GetByProjectId(project.ID);

            Assert.Equal(backlogItems.Count, returnedBacklogItems.Count);
        }

        [Fact]
        public void GetByProjectIdAndBacklogItemTypeId_ReturnsBacklogItems()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var backlogItems = new List<BacklogItem>
            {
                _Fixtures.CreateBacklogItem("Test BacklogItem 1", project: project, backlogItemType: backlogItemType),
                _Fixtures.CreateBacklogItem("Test BacklogItem 2", project: project, backlogItemType: backlogItemType)
            };

            List<BacklogItem> returnedBacklogItems = _backlogItemService
                .GetByProjectIdAndBacklogItemTypeId(project.ID, backlogItemType.ID);

            Assert.Equal(backlogItems.Count, returnedBacklogItems.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItem()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();
            var title = $"{backlogItem.Title} Updated";

            backlogItem.Title = title;
            backlogItem = _backlogItemService.Update(backlogItem);

            Assert.NotNull(backlogItem);
            Assert.Equal(title, backlogItem.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItem()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            _backlogItemService.Delete(backlogItem);

            backlogItem = _backlogItemService.Get(backlogItem.ID);
            Assert.Null(backlogItem);
        }
    }
}
