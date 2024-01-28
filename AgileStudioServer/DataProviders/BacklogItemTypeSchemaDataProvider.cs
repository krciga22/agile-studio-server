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

        public virtual BacklogItemTypeSchemaApiResource? Create(BacklogItemTypeSchemaPostDto dto)
        {
            var project = _DBContext.Project.Find(dto.ProjectId);
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

        public virtual List<BacklogItemTypeSchemaApiResource> List(int projectId)
        {
            List<BacklogItemTypeSchemaApiResource> apiResources = new();

            Project? project = _DBContext.Project.Find(projectId);
            if(project is null){
                return apiResources;
            }

            List<BacklogItemTypeSchema> backlogItemTypeSchemas = _DBContext.BacklogItemTypeSchema.Where(x => x.Project == project).ToList();
            backlogItemTypeSchemas.ForEach(backlogItemTypeSchema => {
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
            if(backlogItemTypeSchema is null){
                return null;
            }

            LoadReferences(backlogItemTypeSchema);

            return backlogItemTypeSchema != null ? new BacklogItemTypeSchemaApiResource(backlogItemTypeSchema) : null;
        }

        public virtual BacklogItemTypeSchemaApiResource? Update(int id, BacklogItemTypeSchemaPatchDto dto)
        {
            var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(id);
            if (backlogItemTypeSchema is null){
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
            if (backlogItemTypeSchema is null){
                return false;
            }

            _DBContext.BacklogItemTypeSchema.Remove(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return true;
        }

        private void LoadReferences(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            _DBContext.Entry(backlogItemTypeSchema).Reference("Project").Load();
        }
    }
}
