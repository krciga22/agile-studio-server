using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class BacklogItemTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeSchemaController _Controller;

        public BacklogItemTypeSchemaControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            BacklogItemTypeSchemaController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void List_ReturnsDtos()
        {
            List<BacklogItemTypeSchema> backlogItemTypeSchemas = new() {
                _Fixtures.CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 1"),
                _Fixtures.CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaDto>? dtos = null;
            IActionResult result = _Controller.List();
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemTypeSchemaDto>;
            }

            Assert.IsType<List<BacklogItemTypeSchemaDto>>(dtos);
            Assert.Equal(backlogItemTypeSchemas.Count, dtos.Count);
        }

        [Fact]
        public void ListBacklogItemTypes_WithId_ReturnsDtos()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            List<BacklogItemType> backlogItemTypes = new() {
                _Fixtures.CreateBacklogItemType(
                    title: "Test Backlog Item Type Schema 1",
                    backlogItemTypeSchema: backlogItemTypeSchema),
                _Fixtures.CreateBacklogItemType(
                    title: "Test Backlog Item Type Schema 2",
                    backlogItemTypeSchema: backlogItemTypeSchema)
            };

            List<BacklogItemTypeSummaryDto>? dtos = null;
            IActionResult result = _Controller.ListBacklogItemTypes(backlogItemTypeSchema.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemTypeSummaryDto>;
            }

            Assert.IsType<List<BacklogItemTypeSummaryDto>>(dtos);
            Assert.Equal(backlogItemTypes.Count, dtos.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            BacklogItemTypeSchemaDto? dto = null;
            IActionResult result = _Controller.Get(backlogItemTypeSchema.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemTypeSchemaDto;
            }

            Assert.IsType<BacklogItemTypeSchemaDto>(dto);
            Assert.Equal(backlogItemTypeSchema.ID, dto.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var postDto = new BacklogItemTypeSchemaPostDto("Test Backlog Item Type Schema");

            BacklogItemTypeSchemaDto? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemTypeSchemaDto;
            }

            Assert.IsType<BacklogItemTypeSchemaDto>(dto);
            Assert.Equal(postDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var title = $"{backlogItemTypeSchema.Title} Updated";
            var patchDto = new BacklogItemTypeSchemaPatchDto(backlogItemTypeSchema.ID, title);

            IActionResult result = _Controller.Patch(backlogItemTypeSchema.ID, patchDto);
            BacklogItemTypeSchemaDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as BacklogItemTypeSchemaDto;
            }

            Assert.IsType<BacklogItemTypeSchemaDto>(dto);
            Assert.Equal(patchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            IActionResult result = _Controller.Delete(backlogItemTypeSchema.ID);

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
