
namespace AgileStudioServer.Application.Models.Hydrators
{
    /// <summary>
    /// Instantiates all model hydrators via dependency 
    /// injection, so that they can register themselves 
    /// with the HydratorRegistry.
    /// </summary>
    /// <seealso cref="HydratorRegistry"/>
    public class HydratorLoader
    {
        public HydratorLoader(IEnumerable<IModelHydrator> modelHydrators)
        {
            
        }
    }
}
