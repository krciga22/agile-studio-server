namespace AgileStudioServer.Core.Hydrator
{
    public class AbstractHydratorRegistry : IHydratorRegistry
    {
        protected List<IHydrator> _Hydrators;

        public AbstractHydratorRegistry()
        {
            _Hydrators = new List<IHydrator>();
        }

        public List<IHydrator> GetHydrators()
        {
            return _Hydrators;
        }

        public void Register(IHydrator hydrator)
        {
            bool isRegistered = _Hydrators.Exists(x => nameof(x).Equals(nameof(hydrator)));
            if (!isRegistered)
            {
                _Hydrators.Add(hydrator);
            }
        }

        public void Unregister(IHydrator hydrator)
        {
            bool isRegistered = _Hydrators.Exists(x => nameof(x).Equals(nameof(hydrator)));
            if (isRegistered)
            {
                _Hydrators.Remove(hydrator);
            }
        }
    }
}
