using AgileStudioCLI.FixtureSets;
using AgileStudioServer.Data;

namespace AgileStudioCLI.Commands
{
    internal class LoadFixturesCommand : AbstractCommand
    {
        private readonly DBContext _DBContext;

        public LoadFixturesCommand(DBContext dbContext)
        {
            _DBContext = dbContext;

            SetName("LoadFixtures");
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            // todo - ask user which fixtures they want to load

            var baseFixtureSet = new BaseFixtureSet();
            baseFixtureSet.LoadFixtures(_DBContext);
        }
    }
}
