using AgileStudioServer.API.Controllers;
using AgileStudioServer.API.DtosNew;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class BacklogItemControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemController _Controller;

        public BacklogItemControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            BacklogItemController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            BacklogItemDto? dto = null;
            IActionResult result = _Controller.Get(backlogItem.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(backlogItem.ID, dto.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            var workflowState = _Fixtures.CreateWorkflowState();
            var postDto = new BacklogItemPostDto("Test Backlog Item Type Schema", project.ID, backlogItemType.ID, workflowState.ID);

            BacklogItemDto? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(postDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();
            var title = $"{backlogItem.Title} Updated";
            var patchDto = new BacklogItemPatchDto(backlogItem.ID, title, backlogItem.WorkflowState.ID);

            IActionResult result = _Controller.Patch(backlogItem.ID, patchDto);
            BacklogItemDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(patchDto.Title, dto.Title);
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
