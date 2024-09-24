
using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Exceptions;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypePostConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypePostDto, BacklogItemType>
    {
        private BacklogItemTypeSchemaService _backlogItemTypeSchemaService;
        private WorkflowService _workflowService;

        public BacklogItemTypePostConverter(
            BacklogItemTypeSchemaService backlogItemTypeSchemaService,
            WorkflowService workflowService)
        {
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
            _workflowService = workflowService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypePostDto) &&
                model == typeof(BacklogItemType);
        }

        public BacklogItemType ConvertToModel(BacklogItemTypePostDto dto)
        {
            BacklogItemType model = new(dto.Title) {
                Description = dto.Description,
                BacklogItemTypeSchema = _backlogItemTypeSchemaService
                    .Get(dto.BacklogItemTypeSchemaId) ??
                    throw new ModelNotFoundException(
                        nameof(BacklogItemTypeSchema), dto.BacklogItemTypeSchemaId.ToString()
                    ),
                Workflow = _workflowService
                    .Get(dto.WorkflowId) ??
                    throw new ModelNotFoundException(
                        nameof(Workflow), dto.WorkflowId.ToString()
                    )
            };

            return model;
        }

        public BacklogItemTypePostDto ConvertToDto(BacklogItemType model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
