
using AgileStudioCLI.Commands;

namespace AgileStudioCLI
{
    internal abstract class AbstractConsoleApp
    {
        private readonly List<AbstractCommand> _Commands;
        private readonly string[] _ExitCommandNames;

        public AbstractConsoleApp()
        {
            _Commands = new List<AbstractCommand>();

            _ExitCommandNames = new string[2];
            _ExitCommandNames[0] = "Exit";
            _ExitCommandNames[1] = "\\d";

            Configure();
        }

        /// <summary>
        /// Start the console app and listen for commands.
        /// </summary>
        public virtual void Start()
        {
            var input = "";
            while (!_ExitCommandNames.Contains(input))
            {
                input = Console.ReadLine();
                if(input is not null)
                {
                    input = input.TrimEnd();

                    if (_ExitCommandNames.Contains(input))
                    {
                        continue;
                    }
                    else if (input != String.Empty)
                    {
                        var command = GetCommand(input);
                        if(command is null)
                        {
                            Console.WriteLine("Command not found");
                        }
                        else
                        {
                            command.Execute(null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// List all registered commands. 
        /// </summary>
        public void ListCommands()
        {
            Console.WriteLine("Available Commands:");

            var allCommandNames = new List<string>();

            foreach (var command in _Commands)
            {
                allCommandNames.Add(command.GetName());
            }

            for(int i = 0; i < _ExitCommandNames.Length; i++)
            {
                allCommandNames.Add(_ExitCommandNames[i]);
            }

            foreach (var commandName in allCommandNames)
            {
                Console.WriteLine($"- {commandName}");
            }

            Console.WriteLine(String.Empty);
        }

        /// <summary>
        /// Override this method to add commands to the console app.
        /// </summary>
        protected abstract void Configure();

        /// <summary>
        /// Get a command by name.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected AbstractCommand? GetCommand(string commandName)
        {
            foreach (var cmd in _Commands)
            {
                if (cmd.GetName() == commandName)
                {
                    return cmd;
                }
            }

            return null;
        }

        /// <summary>
        /// Register a new command to be available in the console app.
        /// </summary>
        /// <param name="command"></param>
        protected void AddCommand(AbstractCommand command)
        {
            var existingCommand = GetCommand(command.GetName());
            if (existingCommand is null)
            {
                _Commands.Add(command);
            }
        }
    }
}
