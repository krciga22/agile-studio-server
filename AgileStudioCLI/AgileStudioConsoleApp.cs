
using AgileStudioCLI.Commands;
using AgileStudioServer;

namespace AgileStudioCLI
{
    internal class AgileStudioConsoleApp : AbstractConsoleApp
    {
        protected override void Configure()
        {
            var dbContext = DBContextFactory.Create();

            AddCommand(new ClearFixturesCommand(dbContext));
            AddCommand(new LoadFixturesCommand(dbContext));
        }

        public override void Start()
        {
            Console.WriteLine("Agile Studio CLI");
            Console.WriteLine(String.Empty);

            ListCommands();

            base.Start();
        }
    }
}
