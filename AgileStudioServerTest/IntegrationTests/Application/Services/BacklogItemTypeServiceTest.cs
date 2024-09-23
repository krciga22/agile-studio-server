using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class BacklogItemTypeServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeService _backlogItemTypeService;

        public BacklogItemTypeServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            BacklogItemTypeService backlogItemTypeService) : base(dbContext, fixtures)
        {
            _backlogItemTypeService = backlogItemTypeService;
        }

        [Fact]
        public void Create_ReturnsBacklogItemType()
        {
            BacklogItemType backlogItemType = new("Test BacklogItemType");
            backlogItemType.BacklogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            backlogItemType.Workflow = _Fixtures.CreateWorkflow();

            backlogItemType = _backlogItemTypeService.Create(backlogItemType);

            Assert.NotNull(backlogItemType);
            Assert.True(backlogItemType.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemType()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            var returnedBacklogItemType = _backlogItemTypeService.Get(backlogItemType.ID);

            Assert.NotNull(returnedBacklogItemType);
            Assert.Equal(backlogItemType.ID, returnedBacklogItemType.ID);
        }

        [Fact]
        public void GetByBacklogItemTypeSchemaId_ReturnsBacklogItemTypes()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var backlogItemTypes = new List<BacklogItemType>
            {
                _Fixtures.CreateBacklogItemType(
                    "Test BacklogItemType 1", 
                    backlogItemTypeSchema: backlogItemTypeSchema
                ),
                _Fixtures.CreateBacklogItemType(
                    "Test BacklogItemType 2", 
                    backlogItemTypeSchema: backlogItemTypeSchema
                )
            };

            List<BacklogItemType> returnedBacklogItemTypes = _backlogItemTypeService
                .GetByBacklogItemTypeSchemaId(backlogItemTypeSchema.ID);

            Assert.Equal(backlogItemTypes.Count, returnedBacklogItemTypes.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemType()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var title = $"{backlogItemType.Title} Updated";

            backlogItemType.Title = title;
            backlogItemType = _backlogItemTypeService.Update(backlogItemType);

            Assert.NotNull(backlogItemType);
            Assert.Equal(title, backlogItemType.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemType()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            _backlogItemTypeService.Delete(backlogItemType);

            backlogItemType = _backlogItemTypeService.Get(backlogItemType.ID);
            Assert.Null(backlogItemType);
        }
    }
}
