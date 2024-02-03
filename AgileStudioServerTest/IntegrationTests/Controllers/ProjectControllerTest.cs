
using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.Dto;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class ProjectControllerTest : AbstractControllerTest
    {
        private readonly ProjectController _Controller;

        public ProjectControllerTest(DBContext dbContext, ProjectController controller) : base(dbContext)
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
            IActionResult result = _Controller.Get(Constants.NonExistantId);
            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetBacklogItemsForProject_WithId_ReturnsApiResources()
        {
            var project = CreateProject("Test Project 1");
            var backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);
            List<BacklogItem> backlogItems = new() {
                CreateBacklogItem(project, backlogItemType, "Test Backlog Item 1"),
                CreateBacklogItem(project, backlogItemType, "Test Backlog Item 2")
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
        public void Post_WithDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();
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
            IActionResult result = _Controller.Delete(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            project.BacklogItemTypeSchema = CreateBacklogItemTypeSchema();
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        private BacklogItemType CreateBacklogItemType(BacklogItemTypeSchema? backlogItemTypeSchema = null, string title = "Test Backlog Item Type")
        {
            if (backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

            var backlogItemType = new BacklogItemType(title)
            {
                BacklogItemTypeSchema = backlogItemTypeSchema
            };

            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }

        private BacklogItem CreateBacklogItem(Project? project = null, BacklogItemType? backlogItemType = null, string title = "Test Backlog Item")
        {
            if (project is null)
            {
                project = CreateProject();
            }

            if (backlogItemType is null)
            {
                backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);
            }

            var backlogItem = new BacklogItem(title)
            {
                Project = project,
                BacklogItemType = backlogItemType
            };
            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }
    }
}
