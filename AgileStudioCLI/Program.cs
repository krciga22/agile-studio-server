using AgileStudioCLI.Commands;
using AgileStudioCLI.Services;

Console.WriteLine("Agile Studio CLI");
Console.WriteLine("");

// show a list of available commands
Console.WriteLine("Available Commands:");
Console.WriteLine("- ClearFixtures");
Console.WriteLine("- LoadFixtures");
Console.WriteLine("");

// ask user to enter a command to execute
Console.WriteLine("Enter a command to execute: ");
var selectedCommand = Console.ReadLine();
Console.WriteLine("");

var dbContext = DBContextFactory.Create();

// execute the selected command
switch (selectedCommand)
{
    case "ClearFixtures":
        new ClearFixtures(dbContext).Execute(null);
        break;

    case "LoadFixtures":
        new LoadFixturesCommand(dbContext).Execute(null);
        break;

    default:
        Console.WriteLine("Command not found");
        break;
}

// wait a few seconds before exiting
Console.WriteLine("");
Console.WriteLine("Exiting...");
Thread.Sleep(2000);