using AgileStudioCLI.Commands;
using AgileStudioCLI.Services;

Console.WriteLine("Agile Studio CLI");
Console.WriteLine("");
Console.WriteLine("Available Commands:");
Console.WriteLine("- ClearFixtures");
Console.WriteLine("- LoadFixtures");
Console.WriteLine("- Exit | \\d");
Console.WriteLine("");

var dbContext = DBContextFactory.Create();

string[] exitCommands = new string[2];
exitCommands[0] = "Exit";
exitCommands[1] = "\\d";

var selectedCommand = "";
while(!exitCommands.Contains(selectedCommand))
{
    selectedCommand = Console.ReadLine();
    switch (selectedCommand)
    {
        case "ClearFixtures":
            new ClearFixtures(dbContext).Execute(null);
            break;

        case "LoadFixtures":
            new LoadFixturesCommand(dbContext).Execute(null);
            break;

        default:
            if (!exitCommands.Contains(selectedCommand))
            {
                Console.WriteLine("Command not found");
            }
            break;
    }
}