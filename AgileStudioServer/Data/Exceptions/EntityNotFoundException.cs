using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Data.Exceptions
{
    public class EntityNotFoundException : AbstractException
    {
        public EntityNotFoundException(string entityClassName, string primaryKey) : base($"Required entity \"{entityClassName}\" with primaryKey \"{primaryKey}\" not found")
        {

        }
    }
}
