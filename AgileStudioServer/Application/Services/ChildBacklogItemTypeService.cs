using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class ChildBacklogItemTypeService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public ChildBacklogItemTypeService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<ChildBacklogItemType> GetByParentTypeId(int parentTypeId)
        {
            List<Data.Entities.ChildBacklogItemType> entities = 
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                    childBacklogItemType.ParentType.ID == parentTypeId
                )
                .ToList();

            return HydrateChildBacklogItemTypeModels(entities, 2);
        }

        public virtual List<ChildBacklogItemType> GetByChildTypeId(int childTypeId)
        {
            List<Data.Entities.ChildBacklogItemType> entities = 
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                childBacklogItemType.ChildType.ID == childTypeId
            ).ToList();

            return HydrateChildBacklogItemTypeModels(entities);
        }

        public virtual ChildBacklogItemType? Get(int id)
        {
            Data.Entities.ChildBacklogItemType? entity = _DBContext.ChildBacklogItemType.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateChildBacklogItemTypeModel(entity);
        }

        public virtual ChildBacklogItemType Create(ChildBacklogItemType childBacklogItemType)
        {
            Data.Entities.ChildBacklogItemType entity = HydrateChildBacklogItemTypeEntity(childBacklogItemType);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateChildBacklogItemTypeModel(entity);
        }

        public virtual ChildBacklogItemType Update(ChildBacklogItemType childBacklogItemType)
        {
            Data.Entities.ChildBacklogItemType entity = HydrateChildBacklogItemTypeEntity(childBacklogItemType);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateChildBacklogItemTypeModel(entity);
        }

        public virtual void Delete(ChildBacklogItemType childBacklogItemType)
        {
            Data.Entities.ChildBacklogItemType entity = HydrateChildBacklogItemTypeEntity(childBacklogItemType);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<ChildBacklogItemType> HydrateChildBacklogItemTypeModels(List<Data.Entities.ChildBacklogItemType> entities, int depth = 3)
        {
            List<ChildBacklogItemType> models = new();

            entities.ForEach(entity => {
                ChildBacklogItemType model = HydrateChildBacklogItemTypeModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private ChildBacklogItemType HydrateChildBacklogItemTypeModel(Data.Entities.ChildBacklogItemType childBacklogItemType, int depth = 3)
        {
            return (ChildBacklogItemType)_Hydrator.Hydrate(
                childBacklogItemType, typeof(ChildBacklogItemType), depth
            );
        }

        private Data.Entities.ChildBacklogItemType HydrateChildBacklogItemTypeEntity(ChildBacklogItemType childBacklogItemType, int depth = 3)
        {
            return (Data.Entities.ChildBacklogItemType)_Hydrator.Hydrate(
                childBacklogItemType, typeof(Data.Entities.ChildBacklogItemType), depth
            );
        }
    }
}
