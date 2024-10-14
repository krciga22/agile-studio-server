using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class BacklogItemTypeControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeController _Controller;

        public BacklogItemTypeControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            BacklogItemTypeController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            BacklogItemTypeDto? dto = null;
            IActionResult result = _Controller.Get(backlogItemType.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(dto);
            Assert.Equal(backlogItemType.ID, dto.ID);
        }

        [Fact]
        public void GetForParentBacklogItemType_WithId_ReturnsDtos()
        {
            var parentBacklogItemType = _Fixtures.CreateBacklogItemType();
            var childBacklogItemType1 = _Fixtures.CreateBacklogItemType();
            var childBacklogItemType2 = _Fixtures.CreateBacklogItemType();

            var childBacklogItemTypes = new List<ChildBacklogItemType>() {
                _Fixtures.CreateChildBacklogItemType(
                    parentType: parentBacklogItemType,    
                    childType: childBacklogItemType1
                ),
                _Fixtures.CreateChildBacklogItemType(
                    parentType: parentBacklogItemType,
                    childType: childBacklogItemType2
                )
            };

            List<BacklogItemTypeDto>? dtos = null;
            IActionResult result = _Controller.GetForParentBacklogItemType(parentBacklogItemType.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemTypeDto>;
            }

            Assert.IsType<List<BacklogItemTypeDto>>(dtos);
            Assert.Equal(childBacklogItemTypes.Count, dtos.Count);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var workflow = _Fixtures.CreateWorkflow();
            var postDto = new BacklogItemTypePostDto("Test Backlog Item Type Schema",
                backlogItemTypeSchema.ID, workflow.ID);

            BacklogItemTypeDto? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(dto);
            Assert.Equal(postDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var title = $"{backlogItemType.Title} Updated";
            var patchDto = new BacklogItemTypePatchDto(backlogItemType.ID, title);

            IActionResult result = _Controller.Patch(backlogItemType.ID, patchDto);
            BacklogItemTypeDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(dto);
            Assert.Equal(patchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            IActionResult result = _Controller.Delete(backlogItemType.ID);

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
