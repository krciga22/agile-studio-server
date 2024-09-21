using AgileStudioServer.API.ApiResources;
using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class BacklogItemDataProvider
    {
        private readonly DBContext _DBContext;

        public BacklogItemDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual BacklogItemApiResource Create(BacklogItemPostDto dto)
        {
            Project project = _DBContext.Project.Find(dto.ProjectId) ??
                throw new EntityNotFoundException(nameof(Project), dto.ProjectId.ToString());

            BacklogItemType backlogItemType = _DBContext.BacklogItemType.Find(dto.BacklogItemTypeId) ??
                throw new EntityNotFoundException(nameof(BacklogItemType), dto.BacklogItemTypeId.ToString());

            WorkflowState workflowState = _DBContext.WorkflowState.Find(dto.WorkflowStateId) ??
                throw new EntityNotFoundException(nameof(WorkflowState), dto.WorkflowStateId.ToString());

            Sprint? sprint = null;
            if (dto.SprintId.HasValue)
            {
                sprint = _DBContext.Sprint.Find(dto.SprintId) ??
                    throw new EntityNotFoundException(nameof(Sprint), ((int)dto.SprintId).ToString());
            }

            Release? release = null;
            if (dto.ReleaseId.HasValue)
            {
                release = _DBContext.Release.Find(dto.ReleaseId) ??
                    throw new EntityNotFoundException(nameof(Release), ((int)dto.ReleaseId).ToString());
            }

            var backlogItem = new BacklogItem(dto.Title)
            {
                Description = dto.Description,
                Project = project,
                BacklogItemType = backlogItemType,
                WorkflowState = workflowState,
                Sprint = sprint,
                Release = release,
            };

            _DBContext.Add(backlogItem);
            _DBContext.SaveChanges();

            LoadReferences(backlogItem);

            return new BacklogItemApiResource(backlogItem);
        }

        public virtual List<BacklogItemApiResource> List()
        {
            List<BacklogItemApiResource> apiResources = new();

            List<BacklogItem> backlogItems = _DBContext.BacklogItem.ToList();
            backlogItems.ForEach(backlogItem =>
            {
                LoadReferences(backlogItem);

                apiResources.Add(
                    new BacklogItemApiResource(backlogItem)
                );
            });

            return apiResources;
        }

        public virtual List<BacklogItemApiResource> ListForProjectId(int projectId)
        {
            List<BacklogItemApiResource> apiResources = new();

            List<BacklogItem> backlogItems = _DBContext.BacklogItem.Where(x => x.Project.ID == projectId).ToList();
            backlogItems.ForEach(backlogItem =>
            {
                LoadReferences(backlogItem);

                apiResources.Add(
                    new BacklogItemApiResource(backlogItem)
                );
            });

            return apiResources;
        }

        public virtual BacklogItemApiResource? Get(int id)
        {
            BacklogItem? backlogItem = _DBContext.BacklogItem.Find(id);
            if (backlogItem is null)
            {
                return null;
            }

            LoadReferences(backlogItem);

            return new BacklogItemApiResource(backlogItem);
        }

        public virtual BacklogItemApiResource Update(int id, BacklogItemPatchDto dto)
        {
            var backlogItem = _DBContext.BacklogItem.Find(id) ??
                throw new EntityNotFoundException(nameof(BacklogItem), id.ToString());

            var workflowState = _DBContext.WorkflowState.Find(dto.WorkflowStateId) ??
                throw new EntityNotFoundException(nameof(WorkflowState), dto.WorkflowStateId.ToString());

            Sprint? sprint = null;
            if (dto.SprintId.HasValue)
            {
                sprint = _DBContext.Sprint.Find(dto.SprintId) ??
                    throw new EntityNotFoundException(nameof(Sprint), ((int)dto.SprintId).ToString());
            }

            Release? release = null;
            if (dto.ReleaseId.HasValue)
            {
                release = _DBContext.Release.Find(dto.ReleaseId) ??
                    throw new EntityNotFoundException(nameof(Release), ((int)dto.ReleaseId).ToString());
            }

            backlogItem.Title = dto.Title;
            backlogItem.Description = dto.Description;
            backlogItem.WorkflowState = workflowState;
            backlogItem.Sprint = sprint;
            backlogItem.Release = release;
            _DBContext.SaveChanges();

            LoadReferences(backlogItem);

            return new BacklogItemApiResource(backlogItem);
        }

        public virtual void Delete(int id)
        {
            var backlogItem = _DBContext.BacklogItem.Find(id) ??
                throw new EntityNotFoundException(nameof(BacklogItem), id.ToString());

            LoadReferences(backlogItem);

            _DBContext.BacklogItem.Remove(backlogItem);
            _DBContext.SaveChanges();
        }

        private void LoadReferences(BacklogItem backlogItem)
        {
            _DBContext.Entry(backlogItem).Reference("CreatedBy").Load();
            _DBContext.Entry(backlogItem).Reference("Project").Load();
            _DBContext.Entry(backlogItem).Reference("BacklogItemType").Load();
            _DBContext.Entry(backlogItem).Reference("WorkflowState").Load();
            _DBContext.Entry(backlogItem).Reference("Sprint").Load();
            _DBContext.Entry(backlogItem).Reference("Release").Load();
        }
    }
}
