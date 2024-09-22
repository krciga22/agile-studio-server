
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public abstract class AbstractModelEntityConverter
    {
        protected DBContext _DBContext;

        public AbstractModelEntityConverter(DBContext dBContext)
        {
            _DBContext = dBContext;
        }
    }
}
