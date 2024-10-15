using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class BacklogItemTypeService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<BacklogItemType> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<Data.Entities.BacklogItemType> entities = _DBContext.BacklogItemType.Where(backlogItemType =>
                backlogItemType.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId).ToList();

            return HydrateBacklogItemTypeModels(entities);
        }

        public virtual BacklogItemType? Get(int id)
        {
            Data.Entities.BacklogItemType? entity = _DBContext.BacklogItemType.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual BacklogItemType Create(BacklogItemType backlogItemType)
        {
            Data.Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual BacklogItemType Update(BacklogItemType backlogItemType)
        {
            Data.Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual void Delete(BacklogItemType backlogItemType)
        {
            Data.Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItemType> HydrateBacklogItemTypeModels(List<Data.Entities.BacklogItemType> entities, int depth = 3)
        {
            List<BacklogItemType> models = new();

            entities.ForEach(entity => {
                BacklogItemType model = HydrateBacklogItemTypeModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItemType HydrateBacklogItemTypeModel(Data.Entities.BacklogItemType backlogItemType, int depth = 3)
        {
            return (BacklogItemType)_Hydrator.Hydrate(
                backlogItemType, typeof(BacklogItemType), depth
            );
        }

        private Data.Entities.BacklogItemType HydrateBacklogItemTypeEntity(BacklogItemType backlogItemType, int depth = 3)
        {
            return (Data.Entities.BacklogItemType)_Hydrator.Hydrate(
                backlogItemType, typeof(Data.Entities.BacklogItemType), depth
            );
        }
    }
}
