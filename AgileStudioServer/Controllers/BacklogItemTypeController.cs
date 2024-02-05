using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Services.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BacklogItemTypeController : ControllerBase
    {
        private readonly BacklogItemTypeDataProvider _DataProvider;

        public BacklogItemTypeController(BacklogItemTypeDataProvider dataProvider)
        {
            _DataProvider = dataProvider;
        }

        [HttpGet("{id}", Name = "GetBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemTypeApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource == null){
                return NotFound();
            }

            return Ok(apiResource);
        }

        [HttpPost(Name = "CreateBacklogItemType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemTypePostDto dto)
        {
            var apiResource = _DataProvider.Create(dto);

            var apiResourceUrl = "";
            if (Url != null){
                // todo
                //apiResourceUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? apiResourceUrl;
            }
            return Created(apiResourceUrl, apiResource);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItemType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemTypePatchDto dto)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            apiResource = _DataProvider.Update(id, dto);
            if (apiResource is null){
                return NotFound();
            }

            return new OkObjectResult(apiResource);
        }

        [HttpDelete("{id}", Name = "DeleteBacklogItemType")]
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