
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypeSchemaConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypeSchemaDto, Application.Models.BacklogItemTypeSchema>
    {
        private UserSummaryConverter _userSummaryConverter;

        public BacklogItemTypeSchemaConverter(UserSummaryConverter userSummaryConverter)
        {
            _userSummaryConverter = userSummaryConverter;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypeSchemaDto) &&
                model == typeof(Application.Models.BacklogItemTypeSchema);
        }

        public Application.Models.BacklogItemTypeSchema ConvertToModel(BacklogItemTypeSchemaDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public BacklogItemTypeSchemaDto ConvertToDto(Application.Models.BacklogItemTypeSchema model)
        {
            var dto = new BacklogItemTypeSchemaDto(model.ID, model.Title, model.CreatedOn)
            {
                Description = model.Description
            };

            if (model.CreatedBy != null) 
            {
                dto.CreatedBy = _userSummaryConverter.ConvertToDto(model.CreatedBy);
            }

            return dto;
        }
    }
}
