using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class WorkflowDataProvider
    {
        private readonly DBContext _DBContext;

        public WorkflowDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<WorkflowDto> List()
        {
            List<Workflow> workflows = _DBContext.Workflow.ToList();

            List<WorkflowDto> workflowApiResources = new();
            workflows.ForEach(workflow =>
            {
                LoadReferences(workflow);

                workflowApiResources.Add(
                    new WorkflowDto(workflow)
                );
            });

            return workflowApiResources;
        }

        public virtual WorkflowDto? Get(int id)
        {
            Workflow? workflow = _DBContext.Workflow.Find(id);
            if (workflow is null)
            {
                return null;
            }

            LoadReferences(workflow);

            return new WorkflowDto(workflow);
        }

        public virtual WorkflowDto Create(WorkflowPostDto workflowPostDto)
        {
            var workflow = new Workflow(workflowPostDto.Title)
            {
                Description = workflowPostDto.Description
            };

            _DBContext.Add(workflow);
            _DBContext.SaveChanges();

            return new WorkflowDto(workflow);
        }

        public virtual WorkflowDto Update(int id, WorkflowPatchDto workflowPatchDto)
        {
            var workflow = _DBContext.Workflow.Find(id) ??
                throw new EntityNotFoundException(nameof(Workflow), id.ToString());

            workflow.Title = workflowPatchDto.Title;
            workflow.Description = workflowPatchDto.Description;
            _DBContext.SaveChanges();

            LoadReferences(workflow);

            return new WorkflowDto(workflow);
        }

        public virtual void Delete(int id)
        {
            var workflow = _DBContext.Workflow.Find(id) ??
                throw new EntityNotFoundException(nameof(Workflow), id.ToString());

            _DBContext.Workflow.Remove(workflow);
            _DBContext.SaveChanges();
        }

        private void LoadReferences(Workflow workflow)
        {
            _DBContext.Entry(workflow).Reference("CreatedBy").Load();
        }
    }
}
