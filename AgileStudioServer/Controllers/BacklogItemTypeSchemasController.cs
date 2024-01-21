using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BacklogItemTypeSchemasController : ControllerBase
    {
        private readonly BacklogItemTypeSchemaDataProvider _DataProvider;

        public BacklogItemTypeSchemasController(BacklogItemTypeSchemaDataProvider dataProvider)
        {
            _DataProvider = dataProvider;
        }

        [HttpGet(Name = "GetBacklogItemTypeSchemas")]
        [ProducesResponseType(typeof(List<BacklogItemTypeSchemaApiResource>), StatusCodes.Status200OK)]
        public IActionResult GetByProject(int projectId)
        {
            return Ok(_DataProvider.List(projectId));
        }

        [HttpGet("{id}", Name = "GetBacklogItemTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource == null){
                return NotFound();
            }

            return Ok(apiResource);
        }

        [HttpPost(Name = "CreateBacklogItemTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemTypeSchemaPostDto dto)
        {
            var apiResource = _DataProvider.Create(dto);

            var apiResourceUrl = "";
            if (Url != null){
                // todo
                //apiResourceUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? apiResourceUrl;
            }
            return Created(apiResourceUrl, apiResource);
        }

        [HttpPatch(Name = "UpdateBacklogItemTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemTypeSchemaPatchDto dto)
        {
            var apiResource = _DataProvider.Update(id, dto);
            if (apiResource is null){
                return NotFound();
            }

            return new OkObjectResult(apiResource);
        }

        [HttpDelete(Name = "DeleteBacklogItemTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null){
                return NotFound();
            }

            var result = _DataProvider.Delete(id);
            if (!result){
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkResult();
        }
    }
}