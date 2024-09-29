
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemTypeSchemaHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private UserHydrator _userHydrator;

        public BacklogItemTypeSchemaHydrator(
            DBContext dbContext,
            UserHydrator userHydrator)
        {
            _DBContext = dbContext;
            _userHydrator = userHydrator;
        }

        public BacklogItemTypeSchema Hydrate(Data.Entities.BacklogItemTypeSchema entity, BacklogItemTypeSchema? model = null)
        {
            if (model == null)
            {
                model = new BacklogItemTypeSchema(entity.Title);
            }

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            return model;
        }

        public BacklogItemTypeSchema Hydrate(API.DtosNew.BacklogItemTypeSchemaPostDto dto, BacklogItemTypeSchema? model = null)
        {
            model ??= new BacklogItemTypeSchema(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public BacklogItemTypeSchema Hydrate(API.DtosNew.BacklogItemTypeSchemaPatchDto dto, BacklogItemTypeSchema? model = null)
        {
            if (model == null)
            {
                Data.Entities.BacklogItemTypeSchema? backlogItemTypeSchemaEntity = 
                    _DBContext.BacklogItemTypeSchema.Find(dto.ID) ??
                        throw new ModelNotFoundException(
                            nameof(BacklogItemTypeSchema), 
                            dto.ID.ToString()
                        );

                model ??= Hydrate(backlogItemTypeSchemaEntity);
            }

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
