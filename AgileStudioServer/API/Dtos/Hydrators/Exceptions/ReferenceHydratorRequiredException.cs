using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators.Exceptions
{
    public class ReferenceHydratorRequiredException : AbstractException
    {
        public ReferenceHydratorRequiredException(IHydrator hydrator) : base($"The hydrator {nameof(hydrator)} requires a reference hydrator")
        {
        
        }
    }
}
