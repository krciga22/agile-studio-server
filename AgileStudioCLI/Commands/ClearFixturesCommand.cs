using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioCLI.Commands
{
    internal class ClearFixturesCommand : AbstractCommand
    {
        private readonly DBContext _DBContext;

        public ClearFixturesCommand(DBContext dbContext)
        {
            _DBContext = dbContext;

            SetName("ClearFixtures");
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            RemoveAllFixtures();
        }

        private async void RemoveAllFixtures()
        {
            await _DBContext.Project.ForEachAsync(x => _DBContext.Project.Remove(x));
            await _DBContext.BacklogItem.ForEachAsync(x => _DBContext.BacklogItem.Remove(x));
            await _DBContext.BacklogItemType.ForEachAsync(x => _DBContext.BacklogItemType.Remove(x));
            await _DBContext.BacklogItemTypeSchema.ForEachAsync(x => _DBContext.BacklogItemTypeSchema.Remove(x));
            await _DBContext.WorkflowState.ForEachAsync(x => _DBContext.WorkflowState.Remove(x));
            await _DBContext.Workflow.ForEachAsync(x => _DBContext.Workflow.Remove(x));
            await _DBContext.User.ForEachAsync(x => _DBContext.User.Remove(x));
            _DBContext.SaveChanges();
        }
    }
}
