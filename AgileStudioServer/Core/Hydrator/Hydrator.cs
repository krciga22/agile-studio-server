namespace AgileStudioServer.Core.Hydrator
{
    /// <summary>
    /// General hydrator that can perform all hydrations. 
    /// It does this by searching the HydratorRegistry for 
    /// applicable hydrators and then executing their 
    /// Hydrate() method.
    /// </summary>
    public class Hydrator : AbstractHydrator
    {
        private HydratorRegistry _HydratorRegistry;

        public Hydrator(HydratorRegistry hydratorRegistry) : base()
        {
            _HydratorRegistry = hydratorRegistry;
        }

        public override bool Supports(Type from, Type to)
        {
            bool isSupported = false;

            if (_HydratorRegistry != null)
            {
                foreach (IHydrator hydrator in _HydratorRegistry.GetHydrators())
                {
                    isSupported = hydrator.Supports(from, to);
                    if (isSupported)
                    {
                        break;
                    }
                }
            }
            

            return isSupported;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (_HydratorRegistry != null)
            {
                foreach (IHydrator hydrator in _HydratorRegistry.GetHydrators())
                {
                    bool isSupported = hydrator.Supports(from.GetType(), to.GetType());
                    if (isSupported)
                    {
                        hydrator.Hydrate(from, to, maxDepth, depth, referenceHydrator ?? this);
                        break;
                    }
                }
            }
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            object? obj = null;

            if (_HydratorRegistry != null)
            {
                foreach (IHydrator hydrator in _HydratorRegistry.GetHydrators())
                {
                    bool isSupported = hydrator.Supports(from.GetType(), to);
                    if (isSupported)
                    {
                        obj = hydrator.Hydrate(from, to, maxDepth, depth, referenceHydrator ?? this);
                        break;
                    }
                }
            }

            if (obj == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return obj;
        }
    }
}
