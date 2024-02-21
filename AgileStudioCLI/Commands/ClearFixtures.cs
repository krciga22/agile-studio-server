using AgileStudioServer;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace AgileStudioCLI.Commands
{
    internal class ClearFixtures : ICommand
    {
        private readonly DBContext _DBContext;

        public event EventHandler? CanExecuteChanged;

        public ClearFixtures(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            await _DBContext.Project.ForEachAsync(x => _DBContext.Project.Remove(x));
            await _DBContext.BacklogItem.ForEachAsync(x => _DBContext.BacklogItem.Remove(x));
            await _DBContext.BacklogItemType.ForEachAsync(x => _DBContext.BacklogItemType.Remove(x));
            await _DBContext.BacklogItemTypeSchema.ForEachAsync(x => _DBContext.BacklogItemTypeSchema.Remove(x));
            await _DBContext.WorkflowState.ForEachAsync(x => _DBContext.WorkflowState.Remove(x));
            await _DBContext.Workflow.ForEachAsync(x => _DBContext.Workflow.Remove(x));
            _DBContext.SaveChanges();
        }
    }
}
