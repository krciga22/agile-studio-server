
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class ProjectSummaryDtoHydrator : AbstractDtoHydrator
    {
        public ProjectSummaryDto Hydrate(Application.Models.Project model, ProjectSummaryDto? dto = null)
        {
            dto ??= new ProjectSummaryDto(model.ID, model.Title);

            dto.ID = model.ID;
            dto.Title = model.Title;

            return dto;
        }
    }
}
