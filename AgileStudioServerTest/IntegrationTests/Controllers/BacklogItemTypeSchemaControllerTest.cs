
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
        public void Post_WithPostDto_ReturnsApiResource()
        {
            var project = CreateProject();

            var dto = new BacklogItemTypeSchemaPostDto() { 
                Title = "Test Backlog Item Type Schema",
                ProjectId = project.ID
            };

            BacklogItemTypeSchemaApiResource? apiResource = null;
            IActionResult result = _BacklogItemTypeSchemasController.Post(dto);
            if (result is CreatedResult createdResult){
                apiResource = createdResult.Value as BacklogItemTypeSchemaApiResource;
            }

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            _DBContext.Projects.Add(project);
            _DBContext.SaveChanges();
            return project;
        }
    }
}
