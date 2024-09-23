
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ProjectConverter : AbstractDtoModelConverter, IDtoModelConverter<ProjectDto, Application.Models.Project>
    {
        private BacklogItemTypeSchemaSummaryConverter _backlogItemTypeSchemaSummaryConverter;
        private UserSummaryConverter _userSummaryConverter;

        public ProjectConverter(
            BacklogItemTypeSchemaSummaryConverter backlogItemTypeSchemaSummaryConverter, 
            UserSummaryConverter userSummaryConverter)
        {
            _backlogItemTypeSchemaSummaryConverter = backlogItemTypeSchemaSummaryConverter;
            _userSummaryConverter = userSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ProjectDto) &&
                model == typeof(Application.Models.Project);
        }

        public Application.Models.Project ConvertToModel(ProjectDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public ProjectDto ConvertToDto(Application.Models.Project model)
        {
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchema = _backlogItemTypeSchemaSummaryConverter
                .ConvertToDto(model.BacklogItemTypeSchema);

            var dto = new ProjectDto(model.ID, model.Title, model.CreatedOn, backlogItemTypeSchema)
            {
                Description = null
            };

            if (model.CreatedBy != null) 
            {
                dto.CreatedBy = _userSummaryConverter.ConvertToDto(model.CreatedBy);
            }

            return dto;
        }
    }
}
