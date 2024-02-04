namespace AgileStudioServer.Exceptions
{
    public class EntityNotFoundException : AbstractException
    {
        public EntityNotFoundException(string entityName, string entityId): base($"Required entity \"{entityName}\" with id \"{entityId}\" not found")
        {

        }
    }
}
