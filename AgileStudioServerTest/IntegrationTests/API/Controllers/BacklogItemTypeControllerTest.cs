using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class BacklogItemTypeControllerTest : AbstractControllerTest
    {
        private const int NON_EXISTANT_ID = 1234567;

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

        [Fact]
        public void GetChildTypes_WithExistingId_ReturnsDtos()
        {
            var parentType = _Fixtures.CreateBacklogItemType();
            var childType1 = _Fixtures.CreateBacklogItemType();
            var childType2 = _Fixtures.CreateBacklogItemType();

            var childBacklogItemTypes = new List<ChildBacklogItemType>() {
                _Fixtures.CreateChildBacklogItemType(
                    parentType: parentType,
                    childType: childType1
                ),
                _Fixtures.CreateChildBacklogItemType(
                    parentType: parentType,
                    childType: childType2
                )
            };

            List<BacklogItemTypeDto>? dtos = null;
            IActionResult result = _Controller.GetChildTypes(parentType.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemTypeDto>;
            }

            Assert.IsType<List<BacklogItemTypeDto>>(dtos);
            Assert.Equal(childBacklogItemTypes.Count, dtos.Count);
        }

        [Fact]
        public void GetChildTypes_WithNonExistingId_ReturnsNotFound()
        {
            IActionResult result = _Controller.GetChildTypes(NON_EXISTANT_ID);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PutChildType_WithNewChildType_ReturnsDto()
        {
            var parentType = _Fixtures.CreateBacklogItemType();
            var childType = _Fixtures.CreateBacklogItemType(
                backlogItemTypeSchema: parentType.BacklogItemTypeSchema
            );

            BacklogItemTypeDto? dto = null;
            IActionResult result = _Controller.PutChildType(
                parentType.ID,
                childType.ID
            );
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(dto);
            Assert.Equal(childType.ID, dto.ID);
        }

        [Fact]
        public void PutChildType_WithExistingChildType_ReturnsDto()
        {
            var parentType = _Fixtures.CreateBacklogItemType();
            var childType = _Fixtures.CreateBacklogItemType(
                backlogItemTypeSchema: parentType.BacklogItemTypeSchema
            );

            _Fixtures.CreateChildBacklogItemType(
                parentType: parentType,
                childType: childType
            );

            BacklogItemTypeDto? dto = null;
            IActionResult result = _Controller.PutChildType(
                parentType.ID,
                childType.ID
            );
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(dto);
            Assert.Equal(childType.ID, dto.ID);
        }

        [Fact]
        public void PutChildType_FromDifferentSchema_ReturnsBadRequest()
        {
            var parentType = _Fixtures.CreateBacklogItemType();
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            IActionResult result = _Controller.PutChildType(
                parentType.ID,
                backlogItemType.ID
            );

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutChildType_WithNonExistantParent_ReturnsNotFound()
        {
            var nonExistantBacklogItemTypeId = NON_EXISTANT_ID;
            var childType = _Fixtures.CreateBacklogItemType();

            IActionResult result = _Controller.PutChildType(
                nonExistantBacklogItemTypeId,
                childType.ID
            );

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PutChildType_WithNonExistantChild_ReturnsNotFound()
        { 
            var parentType = _Fixtures.CreateBacklogItemType();
            var nonExistantBacklogItemTypeId = NON_EXISTANT_ID;

            IActionResult result = _Controller.PutChildType(
                parentType.ID, 
                nonExistantBacklogItemTypeId
            );

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteChildType_WithExistingChildType_ReturnsOk()
        {
            var parentType = _Fixtures.CreateBacklogItemType();
            var childType = _Fixtures.CreateBacklogItemType(
                backlogItemTypeSchema: parentType.BacklogItemTypeSchema
            );

            _Fixtures.CreateChildBacklogItemType(
                parentType: parentType,
                childType: childType
            );

            IActionResult result = _Controller.DeleteChildType(
                parentType.ID,
                childType.ID
            );

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteChildType_WithNonExistingChildType_ReturnsNotFound()
        {
            var parentType = _Fixtures.CreateBacklogItemType();
            var childType = _Fixtures.CreateBacklogItemType(
                backlogItemTypeSchema: parentType.BacklogItemTypeSchema
            );

            IActionResult result = _Controller.DeleteChildType(
                parentType.ID,
                childType.ID
            );

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
