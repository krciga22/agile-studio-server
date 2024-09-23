using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class SprintControllerTest : AbstractControllerTest
    {
        private readonly SprintController _Controller;

        public SprintControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            SprintController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var sprint = _Fixtures.CreateSprint();

            SprintDto? sprintApiResource = null;
            IActionResult result = _Controller.Get(sprint.ID);
            if (result is OkObjectResult okResult)
            {
                sprintApiResource = okResult.Value as SprintDto;
            }

            Assert.IsType<SprintDto>(sprintApiResource);
            Assert.Equal(sprint.ID, sprintApiResource.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();
            var sprintPostDto = new SprintPostDto(project.ID);
            sprintPostDto.Description = "Test Sprint";

            SprintDto? sprintApiResource = null;
            IActionResult result = _Controller.Post(sprintPostDto);
            if (result is CreatedResult createdResult)
            {
                sprintApiResource = createdResult.Value as SprintDto;
            }

            Assert.IsType<SprintDto>(sprintApiResource);
            Assert.Equal(sprintPostDto.Description, sprintApiResource.Description);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var sprint = _Fixtures.CreateSprint();
            var description = $"Test Sprint {sprint.SprintNumber} Updated";
            var sprintPatchDto = new SprintPatchDto()
            {
                Description = description
            };

            IActionResult result = _Controller.Patch(sprint.ID, sprintPatchDto);
            SprintDto? sprintApiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                sprintApiResource = okObjectResult.Value as SprintDto;
            }

            Assert.IsType<SprintDto>(sprintApiResource);
            Assert.Equal(sprintPatchDto.Description, sprintApiResource.Description);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var sprint = _Fixtures.CreateSprint();

            IActionResult result = _Controller.Delete(sprint.ID);

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
