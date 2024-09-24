using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowPatchConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowPatchDto, Workflow>
    {
        private WorkflowService _workflowService;

        public WorkflowPatchConverter(WorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowPatchDto) &&
                model == typeof(Workflow);
        }

        public Workflow ConvertToModel(WorkflowPatchDto dto)
        {
            Workflow? model = _workflowService.Get(dto.ID);
            if(model == null)
            {
                throw new ModelNotFoundException(
                    nameof(Workflow), dto.ID.ToString());
            }

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public WorkflowPatchDto ConvertToDto(Workflow model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
