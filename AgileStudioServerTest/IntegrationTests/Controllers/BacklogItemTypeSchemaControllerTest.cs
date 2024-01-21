
using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.Controllers;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class BacklogItemTypeSchemasControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeSchemasController _BacklogItemTypeSchemasController;

        public BacklogItemTypeSchemasControllerTest(DBContext dbContext, BacklogItemTypeSchemasController backlogItemTypeSchemasController) : base(dbContext)
        {
            _BacklogItemTypeSchemasController = backlogItemTypeSchemasController;
        }

        [Fact]
        public void GetByProject_WithProjectId_ReturnsApiResources()
        {
            var project = CreateProject();

            List<BacklogItemTypeSchema> backlogItemTypeSchemas = new() {
                CreateBacklogItemTypeSchema(project, "Test Backlog Item Type Schema 1"),
                CreateBacklogItemTypeSchema(project, "Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaApiResource>? apiResources = null;
            IActionResult result = _BacklogItemTypeSchemasController.GetByProject(project.ID);
            if (result is OkObjectResult okResult){
                apiResources = okResult.Value as List<BacklogItemTypeSchemaApiResource>;
            }

            Assert.IsType<List<BacklogItemTypeSchemaApiResource>>(apiResources);
            Assert.Equal(backlogItemTypeSchemas.Count, apiResources.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var project = CreateProject();
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema(project);

            BacklogItemTypeSchemaApiResource? apiResource = null;
            IActionResult result = _BacklogItemTypeSchemasController.Get(backlogItemTypeSchema.ID);
            if (result is OkObjectResult okResult){
                apiResource = okResult.Value as BacklogItemTypeSchemaApiResource;
            }

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(backlogItemTypeSchema.ID, apiResource.ID);
        }

        [Fact]
        public void Post_WithPostDto_ReturnsApiResource()
        {
            var project = CreateProject();

            var dto = new BacklogItemTypeSchemaPostDto("Test Backlog Item Type Schema", project.ID);

            BacklogItemTypeSchemaApiResource? apiResource = null;
            IActionResult result = _BacklogItemTypeSchemasController.Post(dto);
            if (result is CreatedResult createdResult){
                apiResource = createdResult.Value as BacklogItemTypeSchemaApiResource;
            }

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithPatchDto_ReturnsApiResource()
        {
            var project = CreateProject();
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema(project);

            var title = $"{backlogItemTypeSchema.Title} Updated";
            var dto = new BacklogItemTypeSchemaPatchDto(title);

            IActionResult result = _BacklogItemTypeSchemasController.Patch(backlogItemTypeSchema.ID, dto);

            BacklogItemTypeSchemaApiResource? apiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                apiResource = okObjectResult.Value as BacklogItemTypeSchemaApiResource;
            }

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var project = CreateProject();
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema(project);

            IActionResult result = _BacklogItemTypeSchemasController.Delete(backlogItemTypeSchema.ID);

            Assert.IsType<OkResult>(result as OkResult);
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
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title) 
            { 
                Project = project
            };
            _DBContext.BacklogItemTypeSchemas.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }
    }
}
