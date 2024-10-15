using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class BacklogItemTypeSchemaService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeSchemaService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<BacklogItemTypeSchema> GetAll()
        {
            List<Data.Entities.BacklogItemTypeSchema> entities = _DBContext.BacklogItemTypeSchema.ToList();

            return HydrateBacklogItemTypeSchemaModels(entities);
        }

        public virtual BacklogItemTypeSchema? Get(int id)
        {
            Data.Entities.BacklogItemTypeSchema? entity = _DBContext.BacklogItemTypeSchema.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual BacklogItemTypeSchema Create(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Data.Entities.BacklogItemTypeSchema entity = 
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual BacklogItemTypeSchema Update(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Data.Entities.BacklogItemTypeSchema entity = 
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual void Delete(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Data.Entities.BacklogItemTypeSchema entity = 
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItemTypeSchema> HydrateBacklogItemTypeSchemaModels(
            List<Data.Entities.BacklogItemTypeSchema> entities, int depth = 3)
        {
            List<BacklogItemTypeSchema> models = new();

            entities.ForEach(entity => {
                BacklogItemTypeSchema model = HydrateBacklogItemTypeSchemaModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItemTypeSchema HydrateBacklogItemTypeSchemaModel(
            Data.Entities.BacklogItemTypeSchema backlogItemTypeSchema, int depth = 3)
        {
            return (BacklogItemTypeSchema)_Hydrator.Hydrate(
                backlogItemTypeSchema, typeof(BacklogItemTypeSchema), depth
            );
        }

        private Data.Entities.BacklogItemTypeSchema HydrateBacklogItemTypeSchemaEntity(
            BacklogItemTypeSchema backlogItemTypeSchema, int depth = 3)
        {
            return (Data.Entities.BacklogItemTypeSchema)_Hydrator.Hydrate(
                backlogItemTypeSchema, typeof(Data.Entities.BacklogItemTypeSchema), depth
            );
        }
    }
}
