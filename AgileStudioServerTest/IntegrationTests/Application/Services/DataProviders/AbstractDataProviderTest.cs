using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public abstract class AbstractDataProviderTest : DBTest
    {
        protected readonly EntityFixtures _Fixtures;

        public AbstractDataProviderTest(DBContext dbContext, EntityFixtures fixtures) : base(dbContext)
        {
            _Fixtures = fixtures;
        }
    }
}
