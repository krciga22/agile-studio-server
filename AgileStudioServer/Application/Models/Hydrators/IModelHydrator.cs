
namespace AgileStudioServer.Application.Models.Hydrators
{
    public interface IModelHydrator
    {
        public bool Supports(Type from, Type to);

        public Object Hydrate(object from, Type to, int maxDepth, int depth);

        public void Hydrate(object from, object to, int maxDepth, int depth);
    }
}
