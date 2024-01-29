using AgileStudioServer.ApiResources;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;

namespace AgileStudioServer.DataProviders
{
    public class BacklogItemTypeDataProvider
    {
        private DBContext _DBContext;

        public BacklogItemTypeDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual BacklogItemTypeApiResource? Create(BacklogItemTypePostDto dto)
        {
            BacklogItemTypeSchema? schema = _DBContext.BacklogItemTypeSchema.Find(dto.BacklogItemTypeSchemaId);
            if (schema is null)
            {
                return null;
            }

            var backlogItemType = new BacklogItemType(dto.Title)
            {
                Description = dto.Description,
                BacklogItemTypeSchema = schema,
            };

            _DBContext.Add(backlogItemType);
            _DBContext.SaveChanges();

            return new BacklogItemTypeApiResource(backlogItemType);
        }

        public virtual List<BacklogItemTypeApiResource> List()
        {
            List<BacklogItemTypeApiResource> apiResources = new();

            List<BacklogItemType> backlogItemTypes = _DBContext.BacklogItemType.ToList();
            backlogItemTypes.ForEach(backlogItemType => {
                LoadReferences(backlogItemType);

                apiResources.Add(
                    new BacklogItemTypeApiResource(backlogItemType)
                );
            });

            return apiResources;
        }

        public virtual List<BacklogItemTypeApiSubResource> ListByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<BacklogItemTypeApiSubResource> apiResources = new();

            List<BacklogItemType> backlogItemTypes = _DBContext.BacklogItemType.Where(x => x.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId).ToList();
            backlogItemTypes.ForEach(backlogItemType => {
                LoadReferences(backlogItemType);

                apiResources.Add(
                    new BacklogItemTypeApiSubResource(backlogItemType)
                );
            });

            return apiResources;
        }

        public virtual BacklogItemTypeApiResource? Get(int id)
        {
            BacklogItemType? backlogItemType = _DBContext.BacklogItemType.Find(id);
            if(backlogItemType is null){
                return null;
            }

            LoadReferences(backlogItemType);

            return new BacklogItemTypeApiResource(backlogItemType);
        }

        public virtual BacklogItemTypeApiResource? Update(int id, BacklogItemTypePatchDto dto)
        {
            var backlogItemType = _DBContext.BacklogItemType.Find(id);
            if (backlogItemType is null){
                return null;
            }

            backlogItemType.Title = dto.Title;
            backlogItemType.Description = dto.Description;
            _DBContext.SaveChanges();

            LoadReferences(backlogItemType);

            return new BacklogItemTypeApiResource(backlogItemType);
        }

        public virtual bool Delete(int id)
        {
            var backlogItemType = _DBContext.BacklogItemType.Find(id);
            if (backlogItemType is null){
                return false;
            }

            _DBContext.BacklogItemType.Remove(backlogItemType);
            _DBContext.SaveChanges();
            return true;
        }

        private void LoadReferences(BacklogItemType backlogItemType)
        {
            _DBContext.Entry(backlogItemType).Reference("BacklogItemTypeSchema").Load();
        }
    }
}
