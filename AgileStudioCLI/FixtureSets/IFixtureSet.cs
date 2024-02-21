using AgileStudioServer;

namespace AgileStudioCLI.FixtureSets
{
    internal interface IFixtureSet
    {
        void LoadFixtures(DBContext dbContext);
    }
}
