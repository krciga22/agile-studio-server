using AgileStudioServer.ApiResources;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;

namespace AgileStudioServer.DataProviders
{
    public class BacklogItemDataProvider
    {
        private DBContext _DBContext;

        public BacklogItemDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual BacklogItemApiResource? Create(BacklogItemPostDto dto)
        {
            Project? project = _DBContext.Project.Find(dto.ProjectId);
            if (project is null)
            {
                return null;
            }

            BacklogItemType? backlogItemType = _DBContext.BacklogItemType.Find(dto.BacklogItemTypeId);
            if (backlogItemType is null)
            {
                return null;
            }

            var backlogItem = new BacklogItem(dto.Title)
            {
                Description = dto.Description,
                Project = project,
                BacklogItemType = backlogItemType,
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
            backlogItems.ForEach(backlogItem => {
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
            backlogItems.ForEach(backlogItem => {
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
            if(backlogItem is null){
                return null;
            }

            LoadReferences(backlogItem);

            return new BacklogItemApiResource(backlogItem);
        }

        public virtual BacklogItemApiResource? Update(int id, BacklogItemPatchDto dto)
        {
            var backlogItem = _DBContext.BacklogItem.Find(id);
            if (backlogItem is null){
                return null;
            }

            backlogItem.Title = dto.Title;
            backlogItem.Description = dto.Description;
            _DBContext.SaveChanges();

            LoadReferences(backlogItem);

            return new BacklogItemApiResource(backlogItem);
        }

        public virtual bool Delete(int id)
        {
            var backlogItem = _DBContext.BacklogItem.Find(id);
            if (backlogItem is null){
                return false;
            }

            LoadReferences(backlogItem);

            _DBContext.BacklogItem.Remove(backlogItem);
            _DBContext.SaveChanges();
            return true;
        }

        private void LoadReferences(BacklogItem backlogItem)
        {
            _DBContext.Entry(backlogItem).Reference("Project").Load();
            _DBContext.Entry(backlogItem).Reference("BacklogItemType").Load();
        }
    }
}
