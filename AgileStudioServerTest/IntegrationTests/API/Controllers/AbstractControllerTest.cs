using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public abstract class AbstractControllerTest : DBTest
    {
        protected readonly EntityFixtures _Fixtures;

        public AbstractControllerTest(DBContext dbContext, EntityFixtures fixtures) : base(dbContext)
        {
            _Fixtures = fixtures;
        }
    }
}
