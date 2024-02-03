using AgileStudioServer.ApiResources;
using AgileStudioServer.Dto;
using AgileStudioServer.Services.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BacklogItemTypeSchemaController : ControllerBase
    {
        private readonly BacklogItemTypeSchemaDataProvider _DataProvider;
        private readonly BacklogItemTypeDataProvider _BacklogItemTypeDataProvider;

        public BacklogItemTypeSchemaController(
            BacklogItemTypeSchemaDataProvider dataProvider, 
            BacklogItemTypeDataProvider backlogItemTypeDataProvider)
        {
            _DataProvider = dataProvider;
            _BacklogItemTypeDataProvider = backlogItemTypeDataProvider;
        }

        [HttpGet(Name = "GetBacklogItemTypeSchemas")]
        [ProducesResponseType(typeof(List<BacklogItemTypeSchemaApiResource>), StatusCodes.Status200OK)]
        public IActionResult List()
        {
            return Ok(_DataProvider.List());
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

        [HttpGet("{id}/BacklogItemTypes", Name = "ListBacklogItemTypeSchema_BacklogItemTypes")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemTypeSubResource>), StatusCodes.Status200OK)]
        public IActionResult ListBacklogItemTypes(int id)
        {
            var apiResource = _BacklogItemTypeDataProvider.ListByBacklogItemTypeSchemaId(id);
            if (apiResource == null)
            {
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