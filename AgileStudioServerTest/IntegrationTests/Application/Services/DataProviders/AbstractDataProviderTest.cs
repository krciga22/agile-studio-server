using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public abstract class AbstractDataProviderTest : DBTest
    {
        protected readonly Fixtures _Fixtures;

        public AbstractDataProviderTest(DBContext dbContext, Fixtures fixtures) : base(dbContext)
        {
            _Fixtures = fixtures;
        }
    }
}
