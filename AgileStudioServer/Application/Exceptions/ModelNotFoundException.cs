using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Exceptions
{
    public class ModelNotFoundException : AbstractException
    {
        public string ModelClassName { get => modelClassName; }

        public string PrimaryKey { get => primaryKey; }

        public ModelNotFoundException(string modelClassName, string primaryKey) : base($"Required model \"{modelClassName}\" with primaryKey \"{primaryKey}\" not found")
        {
            this.modelClassName = modelClassName;
            this.primaryKey = primaryKey;
        }

        private string modelClassName;

        private string primaryKey;
    }
}
