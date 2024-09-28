
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class SprintDtoHydrator : AbstractDtoHydrator
    {
        private ProjectSummaryDtoHydrator _projectSummaryDtoHydrator;
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;

        public SprintDtoHydrator(
            ProjectSummaryDtoHydrator projectSummaryDtoHydrator,
            UserSummaryDtoHydrator userSummaryDtoHydrator)
        {
            _projectSummaryDtoHydrator = projectSummaryDtoHydrator;
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
        }

        public SprintDto Hydrate(Application.Models.Sprint model, SprintDto? dto = null)
        {
            ProjectSummaryDto projectSummaryDto = _projectSummaryDtoHydrator.Hydrate(model.Project);

            dto ??= new SprintDto(model.ID, model.SprintNumber, projectSummaryDto, model.CreatedOn);

            dto.ID = model.ID;
            dto.SprintNumber = model.SprintNumber;
            dto.Description = model.Description;
            dto.CreatedOn = model.CreatedOn;
            dto.StartDate = model.StartDate;
            dto.EndDate = model.EndDate;

            if (model.CreatedBy != null)
            {
                dto.CreatedBy = _userSummaryDtoHydrator.Hydrate(model.CreatedBy);
            }

            return dto;
        }
    }
}
