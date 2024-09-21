using AgileStudioServer.API.Controllers;
using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class BacklogItemControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemController _Controller;

        public BacklogItemControllerTest(
            DBContext dbContext,
            Fixtures fixtures,
            BacklogItemController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            BacklogItemApiResource? apiResource = null;
            IActionResult result = _Controller.Get(backlogItem.ID);
            if (result is OkObjectResult okResult)
            {
                apiResource = okResult.Value as BacklogItemApiResource;
            }

            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(backlogItem.ID, apiResource.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            var workflowState = _Fixtures.CreateWorkflowState();
            var dto = new BacklogItemPostDto("Test Backlog Item Type Schema", project.ID, backlogItemType.ID, workflowState.ID);

            BacklogItemApiResource? apiResource = null;
            IActionResult result = _Controller.Post(dto);
            if (result is CreatedResult createdResult)
            {
                apiResource = createdResult.Value as BacklogItemApiResource;
            }

            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();
            var title = $"{backlogItem.Title} Updated";
            var dto = new BacklogItemPatchDto(backlogItem.ID, title, backlogItem.WorkflowState.ID);

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
            var backlogItem = _Fixtures.CreateBacklogItem();

            IActionResult result = _Controller.Delete(backlogItem.ID);

            Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Delete(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }
    }
}
