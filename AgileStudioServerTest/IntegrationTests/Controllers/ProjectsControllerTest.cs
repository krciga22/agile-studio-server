
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
        private readonly ProjectsController _ProjectsController;

        public ProjectsControllerTest(DBContext dbContext, ProjectsController projectsController) : base(dbContext)
        {
            _ProjectsController = projectsController;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsProjectApiResources()
        {
            List<Project> projects = new () {
                CreateProject("Test Project 1"),
                CreateProject("Test Project 2")
            };

            List<ProjectApiResource>? projectApiResources = null;
            IActionResult result = _ProjectsController.Get();
            if (result is OkObjectResult okResult){
                projectApiResources = okResult.Value as List<ProjectApiResource>;
            }

            Assert.IsType<List<ProjectApiResource>>(projectApiResources);
            Assert.Equal(projects.Count, projectApiResources.Count);
        }

        [Fact]
        public void Get_WithProjectId_ReturnsProjectApiResource()
        {
            var project = CreateProject();

            ProjectApiResource? projectApiResource = null;
            IActionResult result = _ProjectsController.Get(project.ID);
            if (result is OkObjectResult okResult){
                projectApiResource = okResult.Value as ProjectApiResource;
            }

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(project.ID, projectApiResource.ID);
        }

        [Fact]
        public void Get_WithInvalidProjectId_ReturnsNotFoundResult()
        {
            var nonExistantId = 9999;
            IActionResult result = _ProjectsController.Get(nonExistantId);
            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void Post_WithProjectPostDto_ReturnsProjectApiResource()
        {
            var projectPostDto = new ProjectPostDto() { 
                Title = "Test Project"
            };

            ProjectApiResource? projectApiResource = null;
            IActionResult result = _ProjectsController.Post(projectPostDto);
            if (result is CreatedResult createdResult){
                projectApiResource = createdResult.Value as ProjectApiResource;
            }

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(projectPostDto.Title, projectApiResource.Title);
        }

        [Fact]
        public void Patch_WithProjectPatchDto_ReturnsProjectApiResource()
        {
            var project = CreateProject();

            var projectPatchDto = new ProjectPatchDto() { 
                Title = $"{project.Title} Updated"
            };
            IActionResult result = _ProjectsController.Patch(project.ID, projectPatchDto);

            ProjectApiResource? projectApiResource = null;
            if (result is OkObjectResult okObjectResult){
                projectApiResource = okObjectResult.Value as ProjectApiResource;
            }

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(projectPatchDto.Title, projectApiResource.Title);
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
