
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class ProjectDtoHydrator : AbstractDtoHydrator
    {
        private BacklogItemTypeSchemaSummaryDtoHydrator _backlogItemTypeSchemaSummaryDtoHydrator;
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;

        public ProjectDtoHydrator(
            BacklogItemTypeSchemaSummaryDtoHydrator backlogItemTypeSchemaSummaryDtoHydrator,
            UserSummaryDtoHydrator userSummaryDtoHydrator)
        {
            _backlogItemTypeSchemaSummaryDtoHydrator = backlogItemTypeSchemaSummaryDtoHydrator;
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
        }

        public ProjectDto Hydrate(Application.Models.Project model, ProjectDto? dto = null)
        {
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchemaSummaryDto = 
                _backlogItemTypeSchemaSummaryDtoHydrator.Hydrate(model.BacklogItemTypeSchema);

            dto ??= new ProjectDto(model.ID, model.Title, model.CreatedOn, backlogItemTypeSchemaSummaryDto);

            dto.ID = model.ID;
            dto.Title = model.Title;
            dto.Description = model.Description;
            dto.CreatedOn = model.CreatedOn;

            if (model.CreatedBy != null)
            {
                dto.CreatedBy = _userSummaryDtoHydrator.Hydrate(model.CreatedBy);
            }

            return dto;
        }
    }
}
