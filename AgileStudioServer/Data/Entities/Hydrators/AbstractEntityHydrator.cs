
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public abstract class AbstractEntityHydrator : AbstractHydrator
    {
        protected DBContext _DBContext;

        public AbstractEntityHydrator(DBContext dBContext)
        {
            _DBContext = dBContext;
        }
    }
}
