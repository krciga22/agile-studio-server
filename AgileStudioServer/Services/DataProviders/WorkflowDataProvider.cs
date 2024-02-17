using AgileStudioServer.Exceptions;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Services.DataProviders
{
    public class WorkflowDataProvider
    {
        private readonly DBContext _DBContext;

        public WorkflowDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<WorkflowApiResource> List()
        {
            List<Workflow> workflows = _DBContext.Workflow.ToList();

            List<WorkflowApiResource> workflowApiResources = new();
            workflows.ForEach(workflow =>
            {
                LoadReferences(workflow);

                workflowApiResources.Add(
                    new WorkflowApiResource(workflow)
                );
            });

            return workflowApiResources;
        }

        public virtual WorkflowApiResource? Get(int id)
        {
            Workflow? workflow = _DBContext.Workflow.Find(id);
            if (workflow is null)
            {
                return null;
            }

            LoadReferences(workflow);

            return new WorkflowApiResource(workflow);
        }

        public virtual WorkflowApiResource Create(WorkflowPostDto workflowPostDto)
        {
            var workflow = new Workflow(workflowPostDto.Title)
            {
                Description = workflowPostDto.Description
            };

            _DBContext.Add(workflow);
            _DBContext.SaveChanges();

            return new WorkflowApiResource(workflow);
        }

        public virtual WorkflowApiResource Update(int id, WorkflowPatchDto workflowPatchDto)
        {
            var workflow = _DBContext.Workflow.Find(id) ??
                throw new EntityNotFoundException(nameof(Workflow), id.ToString());

            workflow.Title = workflowPatchDto.Title;
            workflow.Description = workflowPatchDto.Description;
            _DBContext.SaveChanges();

            LoadReferences(workflow);

            return new WorkflowApiResource(workflow);
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
            // stub
        }
    }
}
