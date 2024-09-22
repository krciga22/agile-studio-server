using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class BacklogItemTypeSchemaService
    {
        private readonly DBContext _DBContext;

        private readonly BacklogItemTypeSchemaConverter _converter;

        public BacklogItemTypeSchemaService(DBContext dbContext, BacklogItemTypeSchemaConverter backlogItemTypeSchemaConverter)
        {
            _DBContext = dbContext;
            _converter = backlogItemTypeSchemaConverter;
        }

        public virtual List<BacklogItemTypeSchema> GetAll()
        {
            List<Data.Entities.BacklogItemTypeSchema> entities = _DBContext.BacklogItemTypeSchema.ToList();

            List<BacklogItemTypeSchema> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual List<BacklogItemTypeSchema> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<Data.Entities.BacklogItemTypeSchema> entities = _DBContext.BacklogItemTypeSchema.Where(backlogItemTypeSchema => 
                backlogItemTypeSchema.ID == backlogItemTypeSchemaId).ToList();

            List<BacklogItemTypeSchema> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual BacklogItemTypeSchema? Get(int id)
        {
            Data.Entities.BacklogItemTypeSchema? entity = _DBContext.BacklogItemTypeSchema.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual BacklogItemTypeSchema Create(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Data.Entities.BacklogItemTypeSchema entity = _converter.ConvertToEntity(backlogItemTypeSchema);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual BacklogItemTypeSchema Update(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Data.Entities.BacklogItemTypeSchema entity = _converter.ConvertToEntity(backlogItemTypeSchema);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Data.Entities.BacklogItemTypeSchema entity = _converter.ConvertToEntity(backlogItemTypeSchema);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
