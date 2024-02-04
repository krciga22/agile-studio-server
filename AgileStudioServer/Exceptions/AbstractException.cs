namespace AgileStudioServer.Exceptions
{
    public abstract class AbstractException : System.Exception
    {
        public AbstractException(string? message) : base(message)
        {

        }
    }
}
