
using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class ProjectControllerTest : AbstractControllerTest
    {
        private readonly ProjectController _Controller;

        public ProjectControllerTest(
            DBContext dbContext,
            Fixtures fixtures,
            ProjectController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsApiResources()
        {
            List<Project> projects = new () {
                _Fixtures.CreateProject("Test Project 1"),
                _Fixtures.CreateProject("Test Project 2")
            };

            List<ProjectApiResource>? projectApiResources = null;
            IActionResult result = _Controller.Get();
            if (result is OkObjectResult okResult){
                projectApiResources = okResult.Value as List<ProjectApiResource>;
            }

            Assert.IsType<List<ProjectApiResource>>(projectApiResources);
            Assert.Equal(projects.Count, projectApiResources.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();

            ProjectApiResource? projectApiResource = null;
            IActionResult result = _Controller.Get(project.ID);
            if (result is OkObjectResult okResult){
                projectApiResource = okResult.Value as ProjectApiResource;
            }

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(project.ID, projectApiResource.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);
            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetBacklogItemsForProject_WithId_ReturnsApiResources()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                backlogItemTypeSchema: project.BacklogItemTypeSchema);

            List<BacklogItem> backlogItems = new() {
                _Fixtures.CreateBacklogItem(
                    title: "Test Backlog Item 1",
                    project: project,
                    backlogItemType: backlogItemType),
                _Fixtures.CreateBacklogItem(
                    title: "Test Backlog Item 2",
                    project: project,
                    backlogItemType: backlogItemType)
            };

            List<BacklogItemApiResource>? apiResources = null;
            IActionResult result = _Controller.GetBacklogItemsForProject(project.ID);
            if (result is OkObjectResult okResult)
            {
                apiResources = okResult.Value as List<BacklogItemApiResource>;
            }

            Assert.IsType<List<BacklogItemApiResource>>(apiResources);
            Assert.Equal(backlogItems.Count, apiResources.Count);
        }

        [Fact]
        public void GetSprintsForProject_WithId_ReturnsApiResources()
        {
            var project = _Fixtures.CreateProject();

            List<Sprint> sprints = new() {
                _Fixtures.CreateSprint(
                    sprintNumber: 1,
                    project: project),
                _Fixtures.CreateSprint(
                    sprintNumber: 2,
                    project: project)
            };

            List<SprintApiResource>? apiResources = null;
            IActionResult result = _Controller.GetSprintsForProject(project.ID);
            if (result is OkObjectResult okResult)
            {
                apiResources = okResult.Value as List<SprintApiResource>;
            }

            Assert.IsType<List<SprintApiResource>>(apiResources);
            Assert.Equal(sprints.Count, apiResources.Count);
        }

        [Fact]
        public void GetReleasesForProject_WithId_ReturnsApiResources()
        {
            var project = _Fixtures.CreateProject();

            List<Release> releases = new() {
                _Fixtures.CreateRelease(
                    title: "v1.0.0",
                    project: project),
                _Fixtures.CreateRelease(
                    title: "v1.0.1",
                    project: project)
            };

            List<ReleaseApiResource>? apiResources = null;
            IActionResult result = _Controller.GetReleasesForProject(project.ID);
            if (result is OkObjectResult okResult)
            {
                apiResources = okResult.Value as List<ReleaseApiResource>;
            }

            Assert.IsType<List<ReleaseApiResource>>(apiResources);
            Assert.Equal(releases.Count, apiResources.Count);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var projectPostDto = new ProjectPostDto("Test Project", backlogItemTypeSchema.ID);

            ProjectApiResource? projectApiResource = null;
            IActionResult result = _Controller.Post(projectPostDto);
            if (result is CreatedResult createdResult){
                projectApiResource = createdResult.Value as ProjectApiResource;
            }

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(projectPostDto.Title, projectApiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();

            var title = $"{project.Title} Updated";
            var projectPatchDto = new ProjectPatchDto(title);
            IActionResult result = _Controller.Patch(project.ID, projectPatchDto);

            ProjectApiResource? projectApiResource = null;
            if (result is OkObjectResult okObjectResult){
                projectApiResource = okObjectResult.Value as ProjectApiResource;
            }

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(projectPatchDto.Title, projectApiResource.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var project = _Fixtures.CreateProject();

            IActionResult result = _Controller.Delete(project.ID);

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
