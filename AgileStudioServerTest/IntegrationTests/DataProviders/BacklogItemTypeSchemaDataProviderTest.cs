using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.Dto;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.DataProviders
{
    public class BacklogItemTypeSchemaDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemTypeSchemaDataProvider _dataProvider;

        public BacklogItemTypeSchemaDataProviderTest(DBContext dbContext, BacklogItemTypeSchemaDataProvider backlogItemTypeSchemaDataProvider) : base(dbContext)
        {
            _dataProvider = backlogItemTypeSchemaDataProvider;
        }

        [Fact]
        public void CreateBacklogItemTypeSchema_WithPostDto_ReturnsApiResource()
        {
            var dto = new BacklogItemTypeSchemaPostDto("Test Schema");

            var apiResource = _dataProvider.Create(dto);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItemTypeSchemas_WithProjectId_ReturnsApiResources()
        {
            var backlogItemTypeSchemas = new List<BacklogItemTypeSchema>
            {
                CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 1"),
                CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaApiResource> apiResources = _dataProvider.List();

            Assert.Equal(backlogItemTypeSchemas.Count, apiResources.Count);
        }

        [Fact]
        public void GetBacklogItemTypeSchema_ById_ReturnsApiResource()
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();

            var apiResource = _dataProvider.Get(backlogItemTypeSchema.ID);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        [Fact]
        public void UpdateBacklogItemTypeSchema_WithValidDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();

            var title = $"{backlogItemTypeSchema.Title} Updated";
            var dto = new BacklogItemTypeSchemaPatchDto(title);

            var apiResource = _dataProvider.Update(backlogItemTypeSchema.ID, dto);
            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItemTypeSchema_WithValidId_ReturnsTrue()
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();

            bool result = _dataProvider.Delete(backlogItemTypeSchema.ID);
            Assert.True(result);
        }

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
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
