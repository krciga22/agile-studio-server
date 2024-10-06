namespace AgileStudioServer.Core.Hydrator
{
    public interface IHydrator
    {
        public bool Supports(Type from, Type to);

        public object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null);

        public void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null);
    }
}
