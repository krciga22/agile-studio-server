using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Services.DataProviders
{
    public class BacklogItemTypeSchemaDataProvider
    {
        private DBContext _DBContext;

        public BacklogItemTypeSchemaDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual BacklogItemTypeSchemaApiResource? Create(BacklogItemTypeSchemaPostDto dto)
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(dto.Title)
            {
                Description = dto.Description,
            };

            _DBContext.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();

            return new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema);
        }

        public virtual List<BacklogItemTypeSchemaApiResource> List()
        {
            List<BacklogItemTypeSchemaApiResource> apiResources = new();

            List<BacklogItemTypeSchema> backlogItemTypeSchemas = _DBContext.BacklogItemTypeSchema.ToList();
            backlogItemTypeSchemas.ForEach(backlogItemTypeSchema =>
            {
                LoadReferences(backlogItemTypeSchema);

                apiResources.Add(
                    new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema)
                );
            });

            return apiResources;
        }

        public virtual BacklogItemTypeSchemaApiResource? Get(int id)
        {
            BacklogItemTypeSchema? backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(id);
            if (backlogItemTypeSchema is null)
            {
                return null;
            }

            LoadReferences(backlogItemTypeSchema);

            return backlogItemTypeSchema != null ? new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema) : null;
        }

        public virtual BacklogItemTypeSchemaApiResource? Update(int id, BacklogItemTypeSchemaPatchDto dto)
        {
            var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(id);
            if (backlogItemTypeSchema is null)
            {
                return null;
            }

            backlogItemTypeSchema.Title = dto.Title;
            backlogItemTypeSchema.Description = dto.Description;
            _DBContext.SaveChanges();

            LoadReferences(backlogItemTypeSchema);

            return new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema);
        }

        public virtual bool Delete(int id)
        {
            var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(id);
            if (backlogItemTypeSchema is null)
            {
                return false;
            }

            _DBContext.BacklogItemTypeSchema.Remove(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return true;
        }

        private void LoadReferences(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            // stub
        }
    }
}
