
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowDto, Application.Models.Workflow>
    {
        private UserSummaryConverter _userSummaryConverter;

        public WorkflowConverter(UserSummaryConverter userSummaryConverter)
        {
            _userSummaryConverter = userSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowDto) &&
                model == typeof(Application.Models.Workflow);
        }

        public Application.Models.Workflow ConvertToModel(WorkflowDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public WorkflowDto ConvertToDto(Application.Models.Workflow model)
        {
            var dto = new WorkflowDto(model.ID, model.Title, model.CreatedOn)
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
