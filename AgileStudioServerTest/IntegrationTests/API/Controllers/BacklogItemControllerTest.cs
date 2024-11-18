using AgileStudioServer.API.Controllers;
using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using Models = AgileStudioServer.Application.Models;

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
        public void GetChildBacklogItems_WithId_ReturnsDtos()
        {
            var project = _Fixtures.CreateProject();
            var parentBacklogItem = _Fixtures.CreateBacklogItem(
                "Parent Backlog Item",
                project: project
            );
            var childBacklogItemType = _Fixtures.CreateBacklogItemType();
            var childBacklogItem1 = _Fixtures.CreateBacklogItem(
                "Child BacklogItem 1",
                project: project,
                backlogItemType: childBacklogItemType,
                parentBacklogItem: parentBacklogItem
            );
            var childBacklogItem2 = _Fixtures.CreateBacklogItem(
                "Child BacklogItem 2",
                project: project,
                backlogItemType: childBacklogItemType,
                parentBacklogItem: parentBacklogItem
            );

            var childBacklogItems = new List<BacklogItem>
            {
                childBacklogItem1,
                childBacklogItem2
            };

            PaginatedResultsDto<BacklogItemDto, Models.BacklogItem>? results = null;
            IActionResult result = _Controller.GetChildBacklogItems(parentBacklogItem.ID);
            if (result is OkObjectResult okResult)
            {
                results = okResult.Value as PaginatedResultsDto<BacklogItemDto, Models.BacklogItem>;
            }

            Assert.IsType<PaginatedResultsDto<BacklogItemDto, Models.BacklogItem>>(results);
            Assert.Equal(childBacklogItems.Count, results.Items.Count);

            foreach (var dto in results.Items)
            {
                bool isChildBacklogItem = false;
                foreach (var childBacklogItem in childBacklogItems)
                {
                    if (childBacklogItem.ID == dto.ID)
                    {
                        isChildBacklogItem = true;
                        break;
                    }
                }

                Assert.True(isChildBacklogItem);
            }
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
        public void GetParentBacklogItem_WithId_ReturnsDto()
        {
            var project = _Fixtures.CreateProject();
            var parentBacklogItem = _Fixtures.CreateBacklogItem(
                "Parent Backlog Item",
                project: project
            );
            var childBacklogItem = _Fixtures.CreateBacklogItem(
                "Child BacklogItem",
                project: project,
                parentBacklogItem: parentBacklogItem
            );

            BacklogItemDto? dto = null;
            IActionResult result = _Controller.GetParentBacklogItem(childBacklogItem.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(parentBacklogItem.ID, dto.ID);
        }

        [Fact]
        public void GetParentBacklogItem_WithInvalidId_ReturnsNotFoundResult()
        {
            var project = _Fixtures.CreateProject();
            var backlogItem = _Fixtures.CreateBacklogItem(
                "Test BacklogItem",
                project: project
            );

            IActionResult result = _Controller.GetParentBacklogItem(backlogItem.ID);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetParentBacklogItem_WithNonExistantId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.GetParentBacklogItem(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
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
