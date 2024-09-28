
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class ReleaseDtoHydrator : AbstractDtoHydrator
    {
        private ProjectSummaryDtoHydrator _projectSummaryDtoHydrator;
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;

        public ReleaseDtoHydrator(
            ProjectSummaryDtoHydrator projectSummaryDtoHydrator,
            UserSummaryDtoHydrator userSummaryDtoHydrator)
        {
            _projectSummaryDtoHydrator = projectSummaryDtoHydrator;
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
        }

        public ReleaseDto Hydrate(Application.Models.Release model, ReleaseDto? dto = null)
        {
            ProjectSummaryDto projectSummaryDto = _projectSummaryDtoHydrator.Hydrate(model.Project);

            dto ??= new ReleaseDto(model.ID, model.Title, projectSummaryDto, model.CreatedOn);

            dto.ID = model.ID;
            dto.Title = model.Title;
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
