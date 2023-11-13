using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectDataProvider _projectDataProvider;

        public ProjectsController(ProjectDataProvider projectDataProvider)
        {
            _projectDataProvider = projectDataProvider;
        }

        [HttpGet(Name = "GetProjects")]
        public List<ProjectApiResource> Get()
        {
            return _projectDataProvider.GetProjects();
        }

        [HttpPost(Name = "CreateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(ProjectPostDto projectPostDto)
        {
            var projectApiResource = _projectDataProvider.CreateProject(projectPostDto);
            // todo fix project url after creating new endpoint to get a project
            var projectUrl = Url.Action("Get") ?? "";
            return Created(projectUrl, projectApiResource);
        }
    }
}