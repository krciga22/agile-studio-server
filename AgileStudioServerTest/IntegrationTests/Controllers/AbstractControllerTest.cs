using AgileStudioServer;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public abstract class AbstractControllerTest : IDisposable
    {
        protected readonly DBContext _DBContext;

        public AbstractControllerTest(DBContext dbContext)
        {
            _DBContext = dbContext;
            _DBContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _DBContext.Database.RollbackTransaction();
            GC.SuppressFinalize(this);
        }
    }
}
