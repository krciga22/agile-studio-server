using AgileStudioServer.ApiResources;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;

namespace AgileStudioServer.DataProviders
{
    public class BacklogItemTypeSchemaDataProvider
    {
        private DBContext _DBContext;

        public BacklogItemTypeSchemaDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual BacklogItemTypeSchemaApiResource? CreateBacklogItemTypeSchema(BacklogItemTypeSchemaPostDto dto)
        {
            var project = _DBContext.Projects.Find(dto.ProjectId);
            if(project is null){
                return null;
            }

            var backlogItemTypeSchema = new BacklogItemTypeSchema(dto.Title)
            {
                Description = dto.Description,
                Project = project
            };

            _DBContext.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();

            return new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema);
        }
        public virtual List<BacklogItemTypeSchemaApiResource> GetBacklogItemTypeSchemas()
        {
            List<BacklogItemTypeSchema> backlogItemTypeSchemas = _DBContext.BacklogItemTypeSchemas.ToList();

            List<BacklogItemTypeSchemaApiResource> apiResources = new();
            backlogItemTypeSchemas.ForEach(backlogItemTypeSchema => {
                LoadReferences(backlogItemTypeSchema);

                apiResources.Add(
                    new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema)
                );
            });

            return apiResources;
        }

        public virtual BacklogItemTypeSchemaApiResource? GetBacklogItemTypeSchema(int id)
        {
            BacklogItemTypeSchema? backlogItemTypeSchema = _DBContext.BacklogItemTypeSchemas.Find(id);
            if(backlogItemTypeSchema is null){
                return null;
            }

            LoadReferences(backlogItemTypeSchema);

            return backlogItemTypeSchema != null ? new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema) : null;
        }

        public virtual BacklogItemTypeSchemaApiResource? UpdateBacklogItemTypeSchema(int id, BacklogItemTypeSchemaPatchDto dto)
        {
            var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchemas.Find(id);
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

        private void LoadReferences(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            _DBContext.Entry(backlogItemTypeSchema).Reference("Project").Load();
        }
    }
}
