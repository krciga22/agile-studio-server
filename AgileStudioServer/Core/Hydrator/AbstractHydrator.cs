namespace AgileStudioServer.Core.Hydrator
{
    public abstract class AbstractHydrator : IHydrator
    {
        public abstract bool Supports(Type from, Type to);

        public bool Supports(object from, object to)
        {
            return Supports(from.GetType(), to.GetType());
        }

        public abstract object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null);

        public abstract void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null);
    }
}
