
using AgileStudioServer.Core.Hydrator;
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
            Object? entity = null;

            if (to != typeof(User))
            {
                throw new Exception("Unsupported to"); // todo
            }

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
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (to is not User)
            {
                throw new Exception("Unsupported to");
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
