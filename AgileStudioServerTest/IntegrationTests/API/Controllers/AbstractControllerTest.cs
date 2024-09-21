using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public abstract class AbstractControllerTest : DBTest
    {
        protected readonly Fixtures _Fixtures;

        public AbstractControllerTest(DBContext dbContext, Fixtures fixtures) : base(dbContext)
        {
            _Fixtures = fixtures;
        }
    }
}
