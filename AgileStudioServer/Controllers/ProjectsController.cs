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
        private readonly ProjectDataProvider DataProvider;

        public ProjectsController(ProjectDataProvider projectDataProvider)
        {
            DataProvider = projectDataProvider;
        }

        [HttpGet(Name = "GetProjects")]
        [ProducesResponseType(typeof(List<ProjectApiResource>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(DataProvider.GetProjects());
        }

        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var project = DataProvider.GetProject(id);
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
            var projectApiResource = DataProvider.CreateProject(projectPostDto);

            var projectUrl = "";
            if (Url != null){
                projectUrl = Url.Action(nameof(Get), new { id = projectApiResource.ID }) ?? projectUrl;
            }
            return Created(projectUrl, projectApiResource);
        }

        [HttpPatch(Name = "UpdateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, ProjectPatchDto projectPatchDto)
        {
            var projectApiResource = DataProvider.UpdateProject(id, projectPatchDto);
            if(projectApiResource is null){
                return NotFound();
            }
            
            return new OkObjectResult(projectApiResource);
        }

        [HttpDelete(Name = "DeleteProject")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var project = DataProvider.GetProject(id);
            if(project is null){
                return NotFound();
            }

            var result = DataProvider.DeleteProject(id);
            if(!result){
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return new OkResult();
        }
    }
}