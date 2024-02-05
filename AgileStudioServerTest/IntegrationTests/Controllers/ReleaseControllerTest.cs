
using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class ReleaseControllerTest : AbstractControllerTest
    {
        private readonly ReleaseController _Controller;

        public ReleaseControllerTest(
            DBContext dbContext,
            Fixtures fixtures,
            ReleaseController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var release = _Fixtures.CreateRelease();

            ReleaseApiResource? releaseApiResource = null;
            IActionResult result = _Controller.Get(release.ID);
            if (result is OkObjectResult okResult){
                releaseApiResource = okResult.Value as ReleaseApiResource;
            }

            Assert.IsType<ReleaseApiResource>(releaseApiResource);
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

            ReleaseApiResource? releaseApiResource = null;
            IActionResult result = _Controller.Post(releasePostDto);
            if (result is CreatedResult createdResult){
                releaseApiResource = createdResult.Value as ReleaseApiResource;
            }

            Assert.IsType<ReleaseApiResource>(releaseApiResource);
            Assert.Equal(releasePostDto.Title, releaseApiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var release = _Fixtures.CreateRelease("v1.0.0");
            var releasePatchDto = new ReleasePatchDto("v1.0.1");

            IActionResult result = _Controller.Patch(release.ID, releasePatchDto);
            ReleaseApiResource? releaseApiResource = null;
            if (result is OkObjectResult okObjectResult){
                releaseApiResource = okObjectResult.Value as ReleaseApiResource;
            }

            Assert.IsType<ReleaseApiResource>(releaseApiResource);
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
