using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowStatePostConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowStatePostDto, WorkflowState>
    {
        private WorkflowService _workflowService;

        public WorkflowStatePostConverter(WorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowStatePostDto) &&
                model == typeof(WorkflowState);
        }

        public WorkflowState ConvertToModel(WorkflowStatePostDto dto)
        {
            WorkflowState model = new(dto.Title)
            {
                Description = dto.Description,
                Workflow = _workflowService.Get(dto.WorkflowId) ??
                    throw new ModelNotFoundException(
                        nameof(Workflow), dto.WorkflowId.ToString()
                    )
            };

            return model;
        }

        public WorkflowStatePostDto ConvertToDto(WorkflowState model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
