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
        private readonly BacklogItemTypeSchemaDataProvider _backlogItemTypeSchemaDataProvider;

        public BacklogItemTypeSchemasController(BacklogItemTypeSchemaDataProvider backlogItemTypeSchemaDataProvider)
        {
            _backlogItemTypeSchemaDataProvider = backlogItemTypeSchemaDataProvider;
        }

        [HttpPost(Name = "CreateBacklogItemTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemTypeSchemaPostDto dto)
        {
            var apiResource = _backlogItemTypeSchemaDataProvider.CreateBacklogItemTypeSchema(dto);

            var apiResourceUrl = "";
            if (Url != null){
                // todo
                //apiResourceUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? apiResourceUrl;
            }
            return Created(apiResourceUrl, apiResource);
        }
    }
}