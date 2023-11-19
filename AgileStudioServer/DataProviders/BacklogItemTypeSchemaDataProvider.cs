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
    }
}
