
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public abstract class AbstractModelHydrator : AbstractHydrator
    {
        protected DBContext _DBContext;

        public AbstractModelHydrator(DBContext dbContext){
            _DBContext = dbContext;
        }
    }
}
