
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypeConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypeDto, Application.Models.BacklogItemType>
    {
        private BacklogItemTypeSchemaSummaryConverter _backlogItemTypeSchemaSummaryConverter;
        private WorkflowSummaryConverter _workflowSummaryConveter;
        private UserSummaryConverter _userSummaryConverter;

        public BacklogItemTypeConverter(
            BacklogItemTypeSchemaSummaryConverter backlogItemTypeSchemaSummaryConverter,
            WorkflowSummaryConverter workflowSummaryConveter,
            UserSummaryConverter userSummaryConverter)
        {
            _backlogItemTypeSchemaSummaryConverter = backlogItemTypeSchemaSummaryConverter;
            _workflowSummaryConveter = workflowSummaryConveter;
            _userSummaryConverter = userSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypeDto) &&
                model == typeof(Application.Models.BacklogItemType);
        }

        public Application.Models.BacklogItemType ConvertToModel(BacklogItemTypeDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public BacklogItemTypeDto ConvertToDto(Application.Models.BacklogItemType model)
        {
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchemaSummaryDto = _backlogItemTypeSchemaSummaryConverter
                .ConvertToDto(model.BacklogItemTypeSchema);

            WorkflowSummaryDto workflowSummaryDto = _workflowSummaryConveter
                .ConvertToDto(model.Workflow);

            var dto = new BacklogItemTypeDto(model.ID, model.Title, model.CreatedOn, backlogItemTypeSchemaSummaryDto, workflowSummaryDto)
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
