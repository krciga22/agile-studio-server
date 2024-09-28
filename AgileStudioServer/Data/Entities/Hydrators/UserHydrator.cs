
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class UserHydrator : AbstractEntityHydrator, IEntityHydrator<Application.Models.User, User>
    {
        public UserHydrator(DBContext dBContext) : base(dBContext)
        {
        }

        public User Hydrate(Application.Models.User model, User? entity = null)
        {
            if(entity == null)
            {
                if (model.ID > 0)
                {
                    entity = _DBContext.User.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(User),
                            model.ID.ToString()
                        );
                    }
                }
                else
                {
                    entity = new User(model.Email, model.FirstName, model.LastName);
                }
            }

            entity.Email = model.Email;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;

            return entity;
        }
    }
}
