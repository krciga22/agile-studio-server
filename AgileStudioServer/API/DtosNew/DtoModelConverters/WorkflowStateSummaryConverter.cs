
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowStateSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowStateSummaryDto, Application.Models.WorkflowState>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowStateSummaryDto) &&
                model == typeof(Application.Models.WorkflowState);
        }

        public Application.Models.WorkflowState ConvertToModel(WorkflowStateSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public WorkflowStateSummaryDto ConvertToDto(Application.Models.WorkflowState model)
        {
            var dto = new WorkflowStateSummaryDto(model.ID, model.Title);
            return dto;
        }
    }
}
