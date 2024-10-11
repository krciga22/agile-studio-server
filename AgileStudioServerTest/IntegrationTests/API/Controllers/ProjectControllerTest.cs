using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class ProjectControllerTest : AbstractControllerTest
    {
        private readonly ProjectController _Controller;

        public ProjectControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            ProjectController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsDtos()
        {
            List<Project> projects = new() {
                _Fixtures.CreateProject("Test Project 1"),
                _Fixtures.CreateProject("Test Project 2")
            };

            List<ProjectDto>? projectDtos = null;
            IActionResult result = _Controller.Get();
            if (result is OkObjectResult okResult)
            {
                projectDtos = okResult.Value as List<ProjectDto>;
            }

            Assert.IsType<List<ProjectDto>>(projectDtos);
            Assert.Equal(projects.Count, projectDtos.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var project = _Fixtures.CreateProject();

            ProjectDto? projectDto = null;
            IActionResult result = _Controller.Get(project.ID);
            if (result is OkObjectResult okResult)
            {
                projectDto = okResult.Value as ProjectDto;
            }

            Assert.IsType<ProjectDto>(projectDto);
            Assert.Equal(project.ID, projectDto.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetBacklogItemsForProject_WithId_ReturnsDtos()
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

            List<BacklogItemDto>? dtos = null;
            IActionResult result = _Controller.GetBacklogItemsForProject(project.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemDto>;
            }

            Assert.IsType<List<BacklogItemDto>>(dtos);
            Assert.Equal(backlogItems.Count, dtos.Count);
        }

        [Fact]
        public void GetSprintsForProject_WithId_ReturnsDtos()
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

            List<SprintSummaryDto>? dtos = null;
            IActionResult result = _Controller.GetSprintsForProject(project.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<SprintSummaryDto>;
            }

            Assert.IsType<List<SprintSummaryDto>>(dtos);
            Assert.Equal(sprints.Count, dtos.Count);
        }

        [Fact]
        public void GetReleasesForProject_WithId_ReturnsDtos()
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

            List<ReleaseSummaryDto>? dtos = null;
            IActionResult result = _Controller.GetReleasesForProject(project.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<ReleaseSummaryDto>;
            }

            Assert.IsType<List<ReleaseSummaryDto>>(dtos);
            Assert.Equal(releases.Count, dtos.Count);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var projectPostDto = new ProjectPostDto("Test Project", backlogItemTypeSchema.ID);

            ProjectDto? projectDto = null;
            IActionResult result = _Controller.Post(projectPostDto);
            if (result is CreatedResult createdResult)
            {
                projectDto = createdResult.Value as ProjectDto;
            }

            Assert.IsType<ProjectDto>(projectDto);
            Assert.Equal(projectPostDto.Title, projectDto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var project = _Fixtures.CreateProject();
            var title = $"{project.Title} Updated";
            var projectPatchDto = new ProjectPatchDto(project.ID, title);

            IActionResult result = _Controller.Patch(project.ID, projectPatchDto);
            ProjectDto? projectDto = null;
            if (result is OkObjectResult okObjectResult)
            {
                projectDto = okObjectResult.Value as ProjectDto;
            }

            Assert.IsType<ProjectDto>(projectDto);
            Assert.Equal(projectPatchDto.Title, projectDto.Title);
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
