
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowSummaryDto, Application.Models.Workflow>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowSummaryDto) &&
                model == typeof(Application.Models.Workflow);
        }

        public Application.Models.Workflow ConvertToModel(WorkflowSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public WorkflowSummaryDto ConvertToDto(Application.Models.Workflow model)
        {
            var dto = new WorkflowSummaryDto(model.ID, model.Title);
            return dto;
        }
    }
}
