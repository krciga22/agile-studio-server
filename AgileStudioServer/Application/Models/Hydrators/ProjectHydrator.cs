
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ProjectHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private UserHydrator _userHydrator;
        private BacklogItemTypeSchemaHydrator _backlogItemTypeSchemaHydrator;

        public ProjectHydrator(
            DBContext dbContext,
            UserHydrator userHydrator,
            BacklogItemTypeSchemaHydrator backlogItemTypeSchemaHydrator)
        {
            _DBContext = dbContext;
            _userHydrator = userHydrator;
            _backlogItemTypeSchemaHydrator = backlogItemTypeSchemaHydrator;
        }

        public Project Hydrate(Data.Entities.Project entity, Project? model = null)
        {
            model ??= new Project(entity.Title);

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator.Hydrate(entity.BacklogItemTypeSchema);
            }

            return model;
        }

        public Project Hydrate(API.DtosNew.ProjectPostDto dto, Project? model = null)
        {
            model ??= new Project(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            Data.Entities.BacklogItemTypeSchema? backlogItemTypeSchemaEntity = 
                _DBContext.BacklogItemTypeSchema.Find(dto.BacklogItemTypeSchemaId) ??
                    throw new ModelNotFoundException(
                        nameof(Project), 
                        dto.BacklogItemTypeSchemaId.ToString()
                    );

            model.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator.Hydrate(backlogItemTypeSchemaEntity);

            return model;
        }

        public Project Hydrate(API.DtosNew.ProjectPatchDto dto, Project? model = null)
        {
            if (model == null)
            {
                Data.Entities.Project? projectEntity = _DBContext.Project.Find(dto.ID) ??
                    throw new ModelNotFoundException(nameof(Project), dto.ID.ToString());

                model ??= Hydrate(projectEntity);
            }

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
