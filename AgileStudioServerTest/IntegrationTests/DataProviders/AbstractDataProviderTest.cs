using AgileStudioServer;

namespace AgileStudioServerTest.IntegrationTests.DataProviders
{
    public abstract class AbstractDataProviderTest : IDisposable
    {
        protected readonly DBContext _DBContext;

        public AbstractDataProviderTest(DBContext dbContext)
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
