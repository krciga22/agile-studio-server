namespace AgileStudioServer.Core.Hydrator
{
    public class HydratorRegistry : AbstractHydratorRegistry
    {
        public HydratorRegistry(IEnumerable<IHydrator> hydrators) : base()
        {
            hydrators.ToList().ForEach(
                hydrator => Register(hydrator));
        }
    }
}
