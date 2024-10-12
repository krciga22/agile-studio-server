using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Core.Hydrator.Exceptions
{
    public class HydrationNotSupportedException : AbstractException
    {
        public Type From { get => _From; }

        public Type To { get => _To; }

        private readonly Type _From;

        private readonly Type _To;

        public HydrationNotSupportedException(Type from, Type to) : base($"Hydrating with the given From type \"{from}\" and To type \"{to}\" is not supported")
        {
            _From = from;
            _To = to;
        }
    }
}
