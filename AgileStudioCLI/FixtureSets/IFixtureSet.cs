using AgileStudioServer.Data;

namespace AgileStudioCLI.FixtureSets
{
    internal interface IFixtureSet
    {
        void LoadFixtures(DBContext dbContext);
    }
}
