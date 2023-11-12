using AgileStudioServer;

namespace AgileStudioServerTest.IntegrationTests.DataProviders
{
    public abstract class AbstractControllerTest : IDisposable
    {
        private readonly DBContext _DBContext;

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
