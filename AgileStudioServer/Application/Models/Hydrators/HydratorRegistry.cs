
namespace AgileStudioServer.Application.Models.Hydrators
{
    /// <summary>
    /// Registry for all Model Hydrators.
    /// </summary>
    /// <remarks>
    /// Currently requires callers to depend on HydratorLoader
    /// in order for all Model Hydrators to become registered.
    /// </remarks>
    /// <seealso cref="HydratorLoader"/>
    public class HydratorRegistry : IModelHydrator
    {
        private List<IModelHydrator> _ModelHydrators;

        public HydratorRegistry()
        {
            _ModelHydrators = new List<IModelHydrator>();
        }

        public void Register(IModelHydrator modelHydrator)
        {
            bool isRegistered = _ModelHydrators.Exists(x => nameof(x).Equals(nameof(modelHydrator)));
            if (!isRegistered)
            {
                _ModelHydrators.Add(modelHydrator);
            }
        }

        public void Unregister(IModelHydrator modelHydrator)
        {
            bool isRegistered = _ModelHydrators.Exists(x => nameof(x).Equals(nameof(modelHydrator)));
            if (isRegistered)
            {
                _ModelHydrators.Remove(modelHydrator);
            }
        }

        public bool Supports(Type from, Type to)
        {
            bool isSupported = false;

            foreach (IModelHydrator modelHydrator in _ModelHydrators)
            {
                isSupported = modelHydrator.Supports(from, to);
                if (isSupported)
                {
                    break;
                }
            }

            return isSupported;
        }

        public void Hydrate(Object from, Object to, int maxDepth, int depth)
        {
            foreach (IModelHydrator modelHydrator in _ModelHydrators)
            {
                bool isSupported = modelHydrator.Supports(from.GetType(), to.GetType());
                if (isSupported)
                {
                    modelHydrator.Hydrate(from, to, maxDepth, depth);
                    break;
                }
            }
        }

        public Object Hydrate(Object from, Type to, int maxDepth, int depth)
        {
            Object? model = null;
            foreach (IModelHydrator modelHydrator in _ModelHydrators)
            {
                bool isSupported = modelHydrator.Supports(from.GetType(), to);
                if (isSupported)
                {
                    model = modelHydrator.Hydrate(from, to, maxDepth, depth);
                    break;
                }
            }

            if (model == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return model;
        }
    }
}
