
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public abstract class AbstractModelHydrator : IModelHydrator
    {
        protected DBContext _DBContext;
        protected HydratorRegistry _HydratorRegistry;

        public AbstractModelHydrator(
            DBContext dbContext,
            HydratorRegistry hydratorRegistry)
        {
            _DBContext = dbContext;
            _HydratorRegistry = hydratorRegistry;
        }

        public abstract bool Supports(Type from, Type to);

        public bool Supports(Object from, Object to)
        {
            return Supports(from.GetType(), to.GetType());
        }

        public abstract Object Hydrate(object from, Type to, int maxDepth, int depth);

        public abstract void Hydrate(object from, object to, int maxDepth, int depth);
    }
}
