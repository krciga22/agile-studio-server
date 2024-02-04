using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Services.DataProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectDataProvider _DataProvider;

        private readonly BacklogItemDataProvider _BacklogItemDataProvider;

        private readonly SprintDataProvider _SprintDataProvider;

        public ProjectController(
            ProjectDataProvider projectDataProvider, 
            BacklogItemDataProvider backlogItemDataProvider,
            SprintDataProvider sprintDataProvider)
        {
            _DataProvider = projectDataProvider;
            _BacklogItemDataProvider = backlogItemDataProvider;
            _SprintDataProvider = sprintDataProvider;
        }

        [HttpGet(Name = "GetProjects")]
        [ProducesResponseType(typeof(List<ProjectApiResource>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_DataProvider.List());
        }

        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var project = _DataProvider.Get(id);
            if (project == null){
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet("{id}/BacklogItems", Name = "GetProjectBacklogItems")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemApiResource>), StatusCodes.Status200OK)]
        public IActionResult GetBacklogItemsForProject(int id)
        {
            return Ok(_BacklogItemDataProvider.ListForProjectId(id));
        }

        [HttpGet("{id}/Sprints", Name = "GetProjectSprints")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<SprintApiResource>), StatusCodes.Status200OK)]
        public IActionResult GetSprintsForProject(int id)
        {
            return Ok(_SprintDataProvider.ListForProjectId(id));
        }

        [HttpPost(Name = "CreateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(ProjectPostDto projectPostDto)
        {
            var projectApiResource = _DataProvider.Create(projectPostDto);

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
            var projectApiResource = _DataProvider.Update(id, projectPatchDto);
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
            var project = _DataProvider.Get(id);
            if(project is null){
                return NotFound();
            }

            var result = _DataProvider.Delete(id);
            if(!result){
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return new OkResult();
        }
    }
}