using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;

namespace AgileStudioServerTest.IntegrationTests.DataProviders
{
    public class BacklogItemTypeDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemTypeDataProvider _dataProvider;

        public BacklogItemTypeDataProviderTest(DBContext dbContext, BacklogItemTypeDataProvider backlogItemTypeDataProvider) : base(dbContext)
        {
            _dataProvider = backlogItemTypeDataProvider;
        }

        [Fact]
        public void CreateBacklogItemType_WithPostDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            var dto = new BacklogItemTypePostDto("Test Schema", backlogItemTypeSchema.ID);
            var apiResource = _dataProvider.Create(dto);
            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItemType_ById_ReturnsApiResource()
        {
            var backlogItemType = CreateBacklogItemType();

            var apiResource = _dataProvider.Get(backlogItemType.ID);

            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
        }

        [Fact]
        public void UpdateBacklogItemType_WithValidDto_ReturnsApiResource()
        {
            var backlogItemType = CreateBacklogItemType();

            var title = $"{backlogItemType.Title} Updated";
            var dto = new BacklogItemTypePatchDto(title);

            var apiResource = _dataProvider.Update(backlogItemType.ID, dto);
            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItemType_WithValidId_ReturnsTrue()
        {
            var backlogItemType = CreateBacklogItemType();

            bool result = _dataProvider.Delete(backlogItemType.ID);
            Assert.True(result);
        }

        private BacklogItemType CreateBacklogItemType(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();

            var backlogItemType = new BacklogItemType(title);
            backlogItemType.BacklogItemTypeSchema = backlogItemTypeSchema;
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }
    }
}
