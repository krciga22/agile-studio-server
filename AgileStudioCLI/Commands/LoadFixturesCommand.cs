using AgileStudioCLI.FixtureSets;
using AgileStudioServer;
using System.Windows.Input;

namespace AgileStudioCLI.Commands
{
    internal class LoadFixturesCommand : ICommand
    {
        private readonly DBContext _DBContext;

        public event EventHandler? CanExecuteChanged;

        public LoadFixturesCommand(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // todo - ask user which fixtures they want to load

            var baseFixtureSet = new BaseFixtureSet();
            baseFixtureSet.LoadFixtures(_DBContext);
        }
    }
}
