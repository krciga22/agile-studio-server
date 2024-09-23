
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemDto, Application.Models.BacklogItem>
    {
        private ProjectSummaryConverter _projectSummaryConverter;
        private BacklogItemTypeSummaryConverter _backlogItemTypeSummaryConverter;
        private WorkflowStateSummaryConverter _workflowStateSummaryConveter;
        private UserSummaryConverter _userSummaryConverter;
        private SprintSummaryConverter _sprintSummaryConverter;
        private ReleaseSummaryConverter _releaseSummaryConverter;

        public BacklogItemConverter(
            ProjectSummaryConverter projectSummaryConverter,
            BacklogItemTypeSummaryConverter backlogItemTypeSummaryConverter,
            WorkflowStateSummaryConverter workflowStateSummaryConveter,
            UserSummaryConverter userSummaryConverter,
            SprintSummaryConverter sprintSummaryConverter,
            ReleaseSummaryConverter releaseSummaryConverter)
        {
            _projectSummaryConverter = projectSummaryConverter;
            _backlogItemTypeSummaryConverter = backlogItemTypeSummaryConverter;
            _workflowStateSummaryConveter = workflowStateSummaryConveter;
            _userSummaryConverter = userSummaryConverter;
            _sprintSummaryConverter = sprintSummaryConverter;
            _releaseSummaryConverter = releaseSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemDto) &&
                model == typeof(Application.Models.BacklogItem);
        }

        public Application.Models.BacklogItem ConvertToModel(BacklogItemDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public BacklogItemDto ConvertToDto(Application.Models.BacklogItem model)
        {
            ProjectSummaryDto project = _projectSummaryConverter.ConvertToDto(model.Project);

            BacklogItemTypeSummaryDto backlogItemType = _backlogItemTypeSummaryConverter
                .ConvertToDto(model.BacklogItemType);

            WorkflowStateSummaryDto workflowState = _workflowStateSummaryConveter
                .ConvertToDto(model.WorkflowState);

            var dto = new BacklogItemDto(model.ID, model.Title, model.CreatedOn, project, backlogItemType, workflowState)
            {
                Description = model.Description
            };

            if (model.CreatedBy != null) 
            {
                dto.CreatedBy = _userSummaryConverter.ConvertToDto(model.CreatedBy);
            }

            if (model.Sprint != null)
            {
                dto.Sprint = _sprintSummaryConverter.ConvertToDto(model.Sprint);
            }

            if (model.Release != null)
            {
                dto.Release = _releaseSummaryConverter.ConvertToDto(model.Release);
            }

            return dto;
        }
    }
}
