using System.Windows.Input;

namespace AgileStudioCLI.Commands
{
    internal abstract class AbstractCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private string Name = "Untitled Command";

        /// <summary>
        /// Get the name for this command as it would be called from 
        /// the command line.
        /// </summary>
        /// <param name="name"></param>
        public string GetName()
        {
            return Name;
        }

        /// <summary>
        /// Set the name for the command as it would be called from 
        /// the command line.
        /// </summary>
        /// <param name="name"></param>
        protected void SetName(string name)
        {
            Name = name;
        }

        public abstract bool CanExecute(object? parameter);
        public abstract void Execute(object? parameter);
    }
}
