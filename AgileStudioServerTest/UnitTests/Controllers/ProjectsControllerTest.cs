using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.Controllers;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AgileStudioServerTest.UnitTests.Controllers
{
    public class ProjectsControllerTest : IDisposable
    {
        private readonly DBContext _DBContext;

        public ProjectsControllerTest(DBContext dbContext) 
        {
            _DBContext = dbContext;
            _DBContext.Database.BeginTransaction();
        }

        [Fact]
        public void Get_Projects_ReturnsOkResult()
        {
            var projects = new List<ProjectApiResource>
            {
                CreateTestProjectApiResource(),
                CreateTestProjectApiResource()
            };

            var mockProjectDataProvider = new Mock<ProjectDataProvider>(_DBContext);
            mockProjectDataProvider
                .Setup(dataProvider => dataProvider.List())
                .Returns(projects);

            var projectsController = new ProjectsController(mockProjectDataProvider.Object);
            var result = projectsController.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_Project_ReturnsOkResult()
        {
            var project = CreateTestProject();

            var mockProjectDataProvider = new Mock<ProjectDataProvider>(_DBContext);
            mockProjectDataProvider
                .Setup(dataProvider => dataProvider.Get(project.ID))
                .Returns(new ProjectApiResource(project));

            var projectsController = new ProjectsController(mockProjectDataProvider.Object);
            var result = projectsController.Get(project.ID);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_Project_ReturnsNotFoundResult()
        {
            var mockProjectDataProvider = new Mock<ProjectDataProvider>(_DBContext);
            mockProjectDataProvider
                .Setup(dataProvider => dataProvider.Get(1));

            var projectsController = new ProjectsController(mockProjectDataProvider.Object);
            var result = projectsController.Get(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_Project_ReturnsOkResult()
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            var projectPostDto = new ProjectPostDto("Test Project", backlogItemTypeSchema.ID);

            var testProjectApiResource = CreateTestProjectApiResource();

            var mockProjectDataProvider = new Mock<ProjectDataProvider>(_DBContext);
            mockProjectDataProvider
                .Setup(dataProvider => dataProvider.Create(projectPostDto))
                .Returns(testProjectApiResource);

            var projectsController = new ProjectsController(mockProjectDataProvider.Object);

            var result = projectsController.Post(projectPostDto);

            Assert.IsType<CreatedResult>(result);
        }

        private static ProjectApiResource CreateTestProjectApiResource()
        {
            return new ProjectApiResource(CreateTestProject());
        }

        private static Project CreateTestProject()
        {
            return new Project("Test Project") { ID = 1 };
        }

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        public void Dispose()
        {
            _DBContext.Database.RollbackTransaction();
            GC.SuppressFinalize(this);
        }
    }
}