
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ProjectSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<ProjectSummaryDto, Application.Models.Project>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ProjectSummaryDto) &&
                model == typeof(Application.Models.Project);
        }

        public Application.Models.Project ConvertToModel(ProjectSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public ProjectSummaryDto ConvertToDto(Application.Models.Project model)
        {
            var dto = new ProjectSummaryDto(model.ID, model.Title);
            return dto;
        }
    }
}
