
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class BacklogItemTypeSchemaDtoHydrator : AbstractDtoHydrator
    {
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;

        public BacklogItemTypeSchemaDtoHydrator(UserSummaryDtoHydrator userSummaryDtoHydrator)
        {
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
        }

        public BacklogItemTypeSchemaDto Hydrate(Application.Models.BacklogItemTypeSchema model, BacklogItemTypeSchemaDto? dto = null)
        {
            dto ??= new BacklogItemTypeSchemaDto(model.ID, model.Title, model.CreatedOn);

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
