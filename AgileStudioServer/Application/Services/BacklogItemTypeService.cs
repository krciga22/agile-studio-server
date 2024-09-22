using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class BacklogItemTypeService
    {
        private readonly DBContext _DBContext;

        private readonly BacklogItemTypeConverter _converter;

        public BacklogItemTypeService(DBContext dbContext, BacklogItemTypeConverter backlogItemTypeConverter)
        {
            _DBContext = dbContext;
            _converter = backlogItemTypeConverter;
        }

        public virtual List<BacklogItemType> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<Data.Entities.BacklogItemType> entities = _DBContext.BacklogItemType.Where(backlogItemType =>
                backlogItemType.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId).ToList();

            List<BacklogItemType> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual List<BacklogItemType> GetByBacklogItemTypeId(int backlogItemTypeId)
        {
            List<Data.Entities.BacklogItemType> entities = _DBContext.BacklogItemType.Where(backlogItemType => 
                backlogItemType.ID == backlogItemTypeId).ToList();

            List<BacklogItemType> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual BacklogItemType? Get(int id)
        {
            Data.Entities.BacklogItemType? entity = _DBContext.BacklogItemType.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual BacklogItemType Create(BacklogItemType backlogItemType)
        {
            Data.Entities.BacklogItemType entity = _converter.ConvertToEntity(backlogItemType);

            _DBContext.Add(backlogItemType);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual BacklogItemType Update(BacklogItemType backlogItemType)
        {
            Data.Entities.BacklogItemType entity = _converter.ConvertToEntity(backlogItemType);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(BacklogItemType backlogItemType)
        {
            Data.Entities.BacklogItemType entity = _converter.ConvertToEntity(backlogItemType);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
