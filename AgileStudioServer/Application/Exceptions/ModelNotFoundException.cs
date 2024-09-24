using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Exceptions
{
    public class ModelNotFoundException : AbstractException
    {
        public ModelNotFoundException(string modelClassName, string primaryKey) : base($"Required model \"{modelClassName}\" with primaryKey \"{primaryKey}\" not found")
        {

        }
    }
}
