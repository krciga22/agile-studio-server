using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class ReleaseControllerTest : AbstractControllerTest
    {
        private readonly ReleaseController _Controller;

        public ReleaseControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            ReleaseController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var release = _Fixtures.CreateRelease();

            ReleaseDto? releaseApiResource = null;
            IActionResult result = _Controller.Get(release.ID);
            if (result is OkObjectResult okResult)
            {
                releaseApiResource = okResult.Value as ReleaseDto;
            }

            Assert.IsType<ReleaseDto>(releaseApiResource);
            Assert.Equal(release.ID, releaseApiResource.ID);
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
            var releasePostDto = new ReleasePostDto("v1.0.0", project.ID);

            ReleaseDto? releaseApiResource = null;
            IActionResult result = _Controller.Post(releasePostDto);
            if (result is CreatedResult createdResult)
            {
                releaseApiResource = createdResult.Value as ReleaseDto;
            }

            Assert.IsType<ReleaseDto>(releaseApiResource);
            Assert.Equal(releasePostDto.Title, releaseApiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var release = _Fixtures.CreateRelease("v1.0.0");
            var releasePatchDto = new ReleasePatchDto("v1.0.1");

            IActionResult result = _Controller.Patch(release.ID, releasePatchDto);
            ReleaseDto? releaseApiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                releaseApiResource = okObjectResult.Value as ReleaseDto;
            }

            Assert.IsType<ReleaseDto>(releaseApiResource);
            Assert.Equal(releasePatchDto.Title, releaseApiResource.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var release = _Fixtures.CreateRelease();

            IActionResult result = _Controller.Delete(release.ID);

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
