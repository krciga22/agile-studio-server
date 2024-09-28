
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class BacklogItemTypeDtoHydrator : AbstractDtoHydrator
    {
        private BacklogItemTypeSchemaSummaryDtoHydrator _backlogItemTypeSchemaSummaryDtoHydrator;
        private WorkflowSummaryDtoHydrator _workflowSummaryDtoHydrator;
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;

        public BacklogItemTypeDtoHydrator(
            BacklogItemTypeSchemaSummaryDtoHydrator backlogItemTypeSchemaSummaryDtoHydrator,
            WorkflowSummaryDtoHydrator workflowSummaryDtoHydrator,
            UserSummaryDtoHydrator userSummaryDtoHydrator)
        {
            _backlogItemTypeSchemaSummaryDtoHydrator = backlogItemTypeSchemaSummaryDtoHydrator;
            _workflowSummaryDtoHydrator = workflowSummaryDtoHydrator;
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
        }

        public BacklogItemTypeDto Hydrate(Application.Models.BacklogItemType model, BacklogItemTypeDto? dto = null)
        {
            BacklogItemTypeSchemaSummaryDto backlogItemTypeSchemaSummaryDto =
                _backlogItemTypeSchemaSummaryDtoHydrator.Hydrate(model.BacklogItemTypeSchema);

            WorkflowSummaryDto workflowSummaryDto =
                _workflowSummaryDtoHydrator.Hydrate(model.Workflow);

            dto ??= new BacklogItemTypeDto(model.ID, model.Title, model.CreatedOn, backlogItemTypeSchemaSummaryDto, workflowSummaryDto);

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
