using AgileStudioServer;
using AgileStudioServer.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class ProjectsControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProjectsControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_Project_ReturnsCreatedResult()
        {
            var projectPostDto = new ProjectPostDto()
            {
                Title = "Test Project"
            };
            var httpContent = new StringContent(JsonSerializer.Serialize(projectPostDto), System.Text.Encoding.UTF8, "text/json");
            httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MediaTypeNames.Application.Json);

            var client = _factory.CreateClient();
            var response = await client.PostAsync("/Projects", httpContent);

            Assert.Equal(
                (Int32)System.Net.HttpStatusCode.Created,
                (Int32)response.StatusCode
            );
        }

        [Fact]
        public async void Post_Project_ReturnsBadRequestReqult()
        {
            var projectPostDto = new ProjectPostDto()
            {
                Title = null
            };
            var httpContent = new StringContent(
                JsonSerializer.Serialize(projectPostDto), 
                System.Text.Encoding.UTF8, 
                MediaTypeNames.Application.Json
            );
            httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MediaTypeNames.Application.Json);

            var client = _factory.CreateClient();
            var response = await client.PostAsync("/Projects", httpContent);

            Assert.Equal(
                (Int32)System.Net.HttpStatusCode.BadRequest,
                (Int32)response.StatusCode
            );
        }
    }
}
