
using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class BacklogItemControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemController _Controller;

        public BacklogItemControllerTest(DBContext dbContext, BacklogItemController controller) : base(dbContext)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var backlogItem = CreateBacklogItem();

            BacklogItemApiResource? apiResource = null;
            IActionResult result = _Controller.Get(backlogItem.ID);
            if (result is OkObjectResult okResult){
                apiResource = okResult.Value as BacklogItemApiResource;
            }

            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(backlogItem.ID, apiResource.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var project = CreateProject();
            var backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);
            var dto = new BacklogItemPostDto("Test Backlog Item Type Schema", project.ID, backlogItemType.ID);

            BacklogItemApiResource? apiResource = null;
            IActionResult result = _Controller.Post(dto);
            if (result is CreatedResult createdResult){
                apiResource = createdResult.Value as BacklogItemApiResource;
            }

            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var backlogItem = CreateBacklogItem();

            var title = $"{backlogItem.Title} Updated";
            var dto = new BacklogItemPatchDto(title);

            IActionResult result = _Controller.Patch(backlogItem.ID, dto);

            BacklogItemApiResource? apiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                apiResource = okObjectResult.Value as BacklogItemApiResource;
            }

            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItem = CreateBacklogItem();

            IActionResult result = _Controller.Delete(backlogItem.ID);

            Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Delete(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        private BacklogItem CreateBacklogItem(Project? project = null, BacklogItemType? backlogItemType = null, string title = "Test Backlog Item")
        {
            if (project is null)
            {
                project = CreateProject();
            }

            if (backlogItemType is null)
            {
                backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);
            }

            var backlogItem = new BacklogItem(title)
            {
                Project = project,
                BacklogItemType = backlogItemType
            };
            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }

        private BacklogItemType CreateBacklogItemType(BacklogItemTypeSchema? backlogItemTypeSchema = null, string title = "Test Backlog Item Type")
        {
            if (backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

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

        private Project CreateProject(BacklogItemTypeSchema? backlogItemTypeSchema = null, string title = "Test Project")
        {
            if(backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

            var project = new Project(title);
            project.BacklogItemTypeSchema = backlogItemTypeSchema;
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }
    }
}
