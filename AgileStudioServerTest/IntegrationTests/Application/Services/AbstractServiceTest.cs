using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public abstract class AbstractServiceTest : DBTest
    {
        protected readonly ModelFixtures _Fixtures;

        public AbstractServiceTest(DBContext dbContext, ModelFixtures fixtures) : base(dbContext)
        {
            _Fixtures = fixtures;
        }
    }
}
