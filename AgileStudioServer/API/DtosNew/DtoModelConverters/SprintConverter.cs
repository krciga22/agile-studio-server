
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class SprintConverter : AbstractDtoModelConverter, IDtoModelConverter<SprintDto, Application.Models.Sprint>
    {
        private ProjectSummaryConverter _projectSummaryConverter;
        private UserSummaryConverter _userSummaryConverter;

        public SprintConverter(
            ProjectSummaryConverter projectSummaryConverter, 
            UserSummaryConverter userSummaryConverter)
        {
            _projectSummaryConverter = projectSummaryConverter;
            _userSummaryConverter = userSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(SprintDto) &&
                model == typeof(Application.Models.Sprint);
        }

        public Application.Models.Sprint ConvertToModel(SprintDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public SprintDto ConvertToDto(Application.Models.Sprint model)
        {
            ProjectSummaryDto projectSummaryDto = _projectSummaryConverter
                .ConvertToDto(model.Project);

            var dto = new SprintDto(model.ID, model.SprintNumber, projectSummaryDto, model.CreatedOn)
            {
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            };

            if (model.CreatedBy != null) 
            {
                dto.CreatedBy = _userSummaryConverter.ConvertToDto(model.CreatedBy);
            }

            return dto;
        }
    }
}
