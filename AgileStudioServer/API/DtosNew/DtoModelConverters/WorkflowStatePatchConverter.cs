using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowStatePatchConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowStatePatchDto, WorkflowState>
    {
        private WorkflowStateService _workflowStateService;

        public WorkflowStatePatchConverter(WorkflowStateService workflowStateService)
        {
            _workflowStateService = workflowStateService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowStatePatchDto) &&
                model == typeof(WorkflowState);
        }

        public WorkflowState ConvertToModel(WorkflowStatePatchDto dto)
        {
            WorkflowState? model = _workflowStateService.Get(dto.ID);
            if(model == null)
            {
                throw new ModelNotFoundException(
                    nameof(WorkflowState), dto.ID.ToString());
            }

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public WorkflowStatePatchDto ConvertToDto(WorkflowState model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
