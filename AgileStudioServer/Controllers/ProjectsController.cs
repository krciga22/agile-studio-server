using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

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
        [ProducesResponseType(typeof(List<ProjectApiResource>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_projectDataProvider.GetProjects());
        }

        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)] // todo
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var project = _projectDataProvider.GetProject(id);
            if (project == null){
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost(Name = "CreateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(ProjectPostDto projectPostDto)
        {
            var projectApiResource = _projectDataProvider.CreateProject(projectPostDto);

            var urlActionContext = new UrlActionContext() { 
                Action = nameof(Get),
                Values = new { id = projectApiResource.ID },
                Controller = nameof(ProjectsController),
                Protocol = "http",
                Host = "localhost",
            };

            var projectUrl = "";
            if (Url != null){
                projectUrl = Url.Action(urlActionContext) ?? projectUrl;
            }
            return Created(projectUrl, projectApiResource);
        }
    }
}