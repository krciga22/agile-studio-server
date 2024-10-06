namespace AgileStudioServer.Core.Hydrator
{
    public interface IHydratorRegistry
    {
        public List<IHydrator> GetHydrators();

        public void Register(IHydrator hydrator);

        public void Unregister(IHydrator hydrator);
    }
}
