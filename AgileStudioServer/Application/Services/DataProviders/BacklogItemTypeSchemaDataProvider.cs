using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class BacklogItemTypeSchemaDataProvider
    {
        private readonly DBContext _DBContext;

        public BacklogItemTypeSchemaDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual BacklogItemTypeSchemaDto Create(BacklogItemTypeSchemaPostDto dto)
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(dto.Title)
            {
                Description = dto.Description,
            };

            _DBContext.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();

            return new BacklogItemTypeSchemaDto(backlogItemTypeSchema);
        }

        public virtual List<BacklogItemTypeSchemaDto> List()
        {
            List<BacklogItemTypeSchemaDto> apiResources = new();

            List<BacklogItemTypeSchema> backlogItemTypeSchemas = _DBContext.BacklogItemTypeSchema.ToList();
            backlogItemTypeSchemas.ForEach(backlogItemTypeSchema =>
            {
                LoadReferences(backlogItemTypeSchema);

                apiResources.Add(
                    new BacklogItemTypeSchemaDto(backlogItemTypeSchema)
                );
            });

            return apiResources;
        }

        public virtual BacklogItemTypeSchemaDto? Get(int id)
        {
            BacklogItemTypeSchema? backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(id);
            if (backlogItemTypeSchema is null)
            {
                return null;
            }

            LoadReferences(backlogItemTypeSchema);

            return backlogItemTypeSchema != null ? new BacklogItemTypeSchemaDto(backlogItemTypeSchema) : null;
        }

        public virtual BacklogItemTypeSchemaDto Update(int id, BacklogItemTypeSchemaPatchDto dto)
        {
            var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(id) ??
                throw new EntityNotFoundException(nameof(BacklogItemTypeSchema), id.ToString());

            backlogItemTypeSchema.Title = dto.Title;
            backlogItemTypeSchema.Description = dto.Description;
            _DBContext.SaveChanges();

            LoadReferences(backlogItemTypeSchema);

            return new BacklogItemTypeSchemaDto(backlogItemTypeSchema);
        }

        public virtual void Delete(int id)
        {
            var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(id) ??
                throw new EntityNotFoundException(nameof(BacklogItemTypeSchema), id.ToString());

            _DBContext.BacklogItemTypeSchema.Remove(backlogItemTypeSchema);
            _DBContext.SaveChanges();
        }

        private void LoadReferences(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            _DBContext.Entry(backlogItemTypeSchema).Reference("CreatedBy").Load();
        }
    }
}
