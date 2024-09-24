using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public abstract class AbstractControllerNewTest : DBTest
    {
        protected readonly ModelFixtures _Fixtures;

        public AbstractControllerNewTest(DBContext dbContext, ModelFixtures fixtures) : base(dbContext)
        {
            _Fixtures = fixtures;
        }
    }
}
