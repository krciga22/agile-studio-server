
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowStateConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowStateDto, Application.Models.WorkflowState>
    {
        private WorkflowSummaryConverter _workflowSummaryConverter;
        private UserSummaryConverter _userSummaryConverter;

        public WorkflowStateConverter(
            WorkflowSummaryConverter workflowSummaryConverter,
            UserSummaryConverter userSummaryConverter)
        {
            _workflowSummaryConverter = workflowSummaryConverter;
            _userSummaryConverter = userSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowStateDto) &&
                model == typeof(Application.Models.WorkflowState);
        }

        public Application.Models.WorkflowState ConvertToModel(WorkflowStateDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public WorkflowStateDto ConvertToDto(Application.Models.WorkflowState model)
        {
            WorkflowSummaryDto workflowSummaryDto = _workflowSummaryConverter
                .ConvertToDto(model.Workflow);

            var dto = new WorkflowStateDto(model.ID, model.Title, workflowSummaryDto, model.CreatedOn)
            {
                Description = model.Description
            };

            if (model.CreatedBy != null) 
            {
                dto.CreatedBy = _userSummaryConverter.ConvertToDto(model.CreatedBy);
            }

            return dto;
        }
    }
}
