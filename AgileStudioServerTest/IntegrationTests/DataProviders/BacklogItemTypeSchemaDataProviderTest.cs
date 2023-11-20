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

            var dto = new BacklogItemTypeSchemaPostDto() {
                Title = "Test Schema",
                ProjectId = project.ID
            };

            var apiResource = _dataProvider.CreateBacklogItemTypeSchema(dto);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItemTypeSchemas_WithNoArguments_ReturnsApiResources()
        {
            var project = CreateProject();
            var backlogItemTypeSchemas = new List<BacklogItemTypeSchema>
            {
                CreateBacklogItemTypeSchema(project, "Test Backlog Item Type Schema 1"),
                CreateBacklogItemTypeSchema(project, "Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaApiResource> apiResources = _dataProvider.GetBacklogItemTypeSchemas();

            Assert.Equal(backlogItemTypeSchemas.Count, apiResources.Count);
        }

        [Fact]
        public void GetBacklogItemTypeSchema_ById_ReturnsApiResource()
        {
            var project = CreateProject();
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema(project);

            var apiResource = _dataProvider.GetBacklogItemTypeSchema(backlogItemTypeSchema.ID);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            _DBContext.Projects.Add(project);
            _DBContext.SaveChanges();
            return project;
        }

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(Project project, string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title) { 
                Project = project
            };
            _DBContext.BacklogItemTypeSchemas.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }
    }
}
