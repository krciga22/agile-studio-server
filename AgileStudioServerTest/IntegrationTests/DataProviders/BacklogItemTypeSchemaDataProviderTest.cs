using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;

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
            var project = CreateProject();

            var dto = new BacklogItemTypeSchemaPostDto("Test Schema", project.ID);

            var apiResource = _dataProvider.Create(dto);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItemTypeSchemas_WithProjectId_ReturnsApiResources()
        {
            var project = CreateProject();
            var backlogItemTypeSchemas = new List<BacklogItemTypeSchema>
            {
                CreateBacklogItemTypeSchema(project, "Test Backlog Item Type Schema 1"),
                CreateBacklogItemTypeSchema(project, "Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaApiResource> apiResources = _dataProvider.List(project.ID);

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

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(Project? project = null, string title = "Test Backlog Item Type Schema")
        {
            if (project is null){
                project = CreateProject();
            }

            var backlogItemTypeSchema = new BacklogItemTypeSchema(title)
            {
                Project = project
            };
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }
    }
}
