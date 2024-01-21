
using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.Controllers;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class ProjectsControllerTest : AbstractControllerTest
    {
        private readonly ProjectsController _Controller;

        private readonly int _NonExistantProjectId = 9999;

        public ProjectsControllerTest(DBContext dbContext, ProjectsController controller) : base(dbContext)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsApiResources()
        {
            List<Project> projects = new () {
                CreateProject("Test Project 1"),
                CreateProject("Test Project 2")
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
            var project = CreateProject();

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
            var nonExistantId = _NonExistantProjectId;
            IActionResult result = _Controller.Get(nonExistantId);
            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var projectPostDto = new ProjectPostDto("Test Project");

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
            var project = CreateProject();

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
            var project = CreateProject();

            IActionResult result = _Controller.Delete(project.ID);

            Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Delete(_NonExistantProjectId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            _DBContext.Projects.Add(project);
            _DBContext.SaveChanges();
            return project;
        }
    }
}
