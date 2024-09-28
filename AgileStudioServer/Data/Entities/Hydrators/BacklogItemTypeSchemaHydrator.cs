
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class BacklogItemTypeSchemaHydrator : AbstractEntityHydrator
    {
        private UserHydrator _userHydrator;

        public BacklogItemTypeSchemaHydrator(
            DBContext dBContext, 
            UserHydrator userHydrator) : base(dBContext)
        {
            _userHydrator = userHydrator;
        }

        public BacklogItemTypeSchema Hydrate(Application.Models.BacklogItemTypeSchema model, BacklogItemTypeSchema? entity = null)
        {
            if(entity == null)
            {
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemTypeSchema.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemTypeSchema),
                            model.ID.ToString()
                        );
                    }

                }
                else
                {
                    entity = new BacklogItemTypeSchema(model.Title);
                }
            }
            
            entity.Title = model.Title;

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = _userHydrator.Hydrate(model.CreatedBy);
            }

            return entity;
        }
    }
}
