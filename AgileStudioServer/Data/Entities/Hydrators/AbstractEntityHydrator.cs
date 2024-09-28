
namespace AgileStudioServer.Data.Entities.Hydrators
{
    public abstract class AbstractEntityHydrator
    {
        protected DBContext _DBContext;

        public AbstractEntityHydrator(DBContext dBContext)
        {
            _DBContext = dBContext;
        }
    }
}
