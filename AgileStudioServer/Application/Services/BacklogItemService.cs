using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class BacklogItemService
    {
        private readonly DBContext _DBContext;

        private readonly BacklogItemConverter _converter;

        public BacklogItemService(DBContext dbContext, BacklogItemConverter backlogItemConverter)
        {
            _DBContext = dbContext;
            _converter = backlogItemConverter;
        }

        public virtual List<BacklogItem> GetByProjectId(int projectId)
        {
            List<Data.Entities.BacklogItem> entities = _DBContext.BacklogItem.Where(backlogItem => 
                backlogItem.Project.ID == projectId).ToList();

            List<BacklogItem> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual List<BacklogItem> GetByProjectIdAndBacklogItemTypeId(int projectId, int backlogItemTypeId)
        {
            List<Data.Entities.BacklogItem> entities = _DBContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId && backlogItem.BacklogItemType.ID == backlogItemTypeId).ToList();

            List<BacklogItem> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual BacklogItem? Get(int id)
        {
            Data.Entities.BacklogItem? entity = _DBContext.BacklogItem.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual BacklogItem Create(BacklogItem backlogItem)
        {
            Data.Entities.BacklogItem entity = _converter.ConvertToEntity(backlogItem);

            _DBContext.Add(backlogItem);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual BacklogItem Update(BacklogItem backlogItem)
        {
            Data.Entities.BacklogItem entity = _converter.ConvertToEntity(backlogItem);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(BacklogItem backlogItem)
        {
            Data.Entities.BacklogItem entity = _converter.ConvertToEntity(backlogItem);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
