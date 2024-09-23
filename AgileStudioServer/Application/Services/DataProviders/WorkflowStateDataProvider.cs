using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class WorkflowStateDataProvider
    {
        private readonly DBContext _DBContext;

        public WorkflowStateDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<WorkflowStateDto> ListForWorkflowId(int workflowId)
        {
            List<WorkflowState> workflowStates = _DBContext.WorkflowState.
                Where(x => x.Workflow.ID == workflowId).ToList();

            List<WorkflowStateDto> workflowStateApiResources = new();
            workflowStates.ForEach(workflowState =>
            {
                LoadReferences(workflowState);

                workflowStateApiResources.Add(
                    new WorkflowStateDto(workflowState)
                );
            });

            return workflowStateApiResources;
        }

        public virtual WorkflowStateDto? Get(int id)
        {
            WorkflowState? workflowState = _DBContext.WorkflowState.Find(id);
            if (workflowState is null)
            {
                return null;
            }

            LoadReferences(workflowState);

            return new WorkflowStateDto(workflowState);
        }

        public virtual WorkflowStateDto Create(WorkflowStatePostDto workflowStatePostDto)
        {
            int workflowId = workflowStatePostDto.WorkflowId;
            var workflow = _DBContext.Workflow.Find(workflowId) ??
                throw new EntityNotFoundException(nameof(Workflow), workflowId.ToString());

            var workflowState = new WorkflowState(workflowStatePostDto.Title)
            {
                Workflow = workflow,
                Description = workflowStatePostDto.Description
            };

            _DBContext.Add(workflowState);
            _DBContext.SaveChanges();

            return new WorkflowStateDto(workflowState);
        }

        public virtual WorkflowStateDto Update(int id, WorkflowStatePatchDto workflowStatePatchDto)
        {
            var workflowState = _DBContext.WorkflowState.Find(id) ??
                throw new EntityNotFoundException(nameof(WorkflowState), id.ToString());

            workflowState.Title = workflowStatePatchDto.Title;
            workflowState.Description = workflowStatePatchDto.Description;
            _DBContext.SaveChanges();

            LoadReferences(workflowState);

            return new WorkflowStateDto(workflowState);
        }

        public virtual void Delete(int id)
        {
            var workflowState = _DBContext.WorkflowState.Find(id) ??
                throw new EntityNotFoundException(nameof(WorkflowState), id.ToString());

            _DBContext.WorkflowState.Remove(workflowState);
            _DBContext.SaveChanges();
        }

        private void LoadReferences(WorkflowState workflowState)
        {
            _DBContext.Entry(workflowState).Reference("Workflow").Load();
            _DBContext.Entry(workflowState).Reference("CreatedBy").Load();
        }
    }
}
