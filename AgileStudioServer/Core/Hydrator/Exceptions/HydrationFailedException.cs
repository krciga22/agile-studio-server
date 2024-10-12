using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Core.Hydrator.Exceptions
{
    public class HydrationFailedException : AbstractException
    {
        public Type From { get => _From; }

        public Type To { get => _To; }

        private readonly Type _From;

        private readonly Type _To;

        public HydrationFailedException(Type from, Type to) : base($"Hydrating failed with the given From type \"{from}\" and To type \"{to}\"")
        {
            _From = from;
            _To = to;
        }
    }
}
