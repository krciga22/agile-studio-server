
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class UserHydrator : AbstractEntityHydrator
    {
        public UserHydrator(DBContext dBContext) : base(dBContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.User) &&
                to == typeof(User);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? entity = null;

            if (from is Application.Models.User)
            {
                var model = (Application.Models.User)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.User.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(User), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new User(model.Email, model.FirstName, model.LastName);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }

            if (entity == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupported(from.GetType(), to.GetType());
            }

            var entity = (User)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.User)
            {
                var model = (Application.Models.User)from;

                entity.ID = model.ID;
                entity.Email = model.Email;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.CreatedOn = model.CreatedOn;
            }
        }
    }
}
