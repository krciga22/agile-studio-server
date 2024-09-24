
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ReleaseConverter : AbstractDtoModelConverter, IDtoModelConverter<ReleaseDto, Application.Models.Release>
    {
        private ProjectSummaryConverter _projectSummaryConverter;
        private UserSummaryConverter _userSummaryConverter;

        public ReleaseConverter(
            ProjectSummaryConverter projectSummaryConverter, 
            UserSummaryConverter userSummaryConverter)
        {
            _projectSummaryConverter = projectSummaryConverter;
            _userSummaryConverter = userSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ReleaseDto) &&
                model == typeof(Application.Models.Release);
        }

        public Application.Models.Release ConvertToModel(ReleaseDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public ReleaseDto ConvertToDto(Application.Models.Release model)
        {
            ProjectSummaryDto projectSummaryDto = _projectSummaryConverter
                .ConvertToDto(model.Project);

            var dto = new ReleaseDto(model.ID, model.Title, projectSummaryDto, model.CreatedOn)
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
