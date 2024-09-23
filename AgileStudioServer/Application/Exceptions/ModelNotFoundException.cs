using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Data.Exceptions
{
    public class ModelNotFoundException : AbstractException
    {
        public ModelNotFoundException(string modelClassName, string primaryKey) : base($"Required model \"{modelClassName}\" with primaryKey \"{primaryKey}\" not found")
        {

        }
    }
}
