
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class UserHydrator : AbstractModelHydrator
    {
        public UserHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Data.Entities.User)
            ) && to == typeof(User);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            Object? model = null;

            if (from is int)
            {
                var user = _DBContext.User.Find(from);
                if (user != null)
                {
                    from = user;
                }
            }

            if (from is Data.Entities.User)
            {
                var entity = (Data.Entities.User)from;
                model = new User(entity.Email, entity.FirstName, entity.LastName);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }

            if(model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            if (from is Data.Entities.User && to is User)
            {
                var entity = (Data.Entities.User) from;
                var model = (User) to;

                model.ID = entity.ID;
                model.Email = entity.Email;
                model.FirstName = entity.FirstName;
                model.LastName = entity.LastName;
                model.CreatedOn = entity.CreatedOn;
            }
        }
    }
}
