
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ProjectHydrator : AbstractModelHydrator
    {
        public ProjectHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Data.Entities.Project) || 
                from == typeof(API.Dtos.ProjectPostDto) || 
                from == typeof(API.Dtos.ProjectPatchDto)
            ) && to == typeof(Project);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? model = null;

            if (from is int)
            {
                var project = _DBContext.Project.Find(from);
                if (project != null)
                {
                    from = project;
                }
            }

            if (from is Data.Entities.Project)
            {
                var entity = (Data.Entities.Project)from;
                model = new Project(entity.Title, entity.BacklogItemTypeSchemaID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.ProjectPostDto)
            {
                var dto = (API.Dtos.ProjectPostDto)from;
                model = new Project(dto.Title, dto.BacklogItemTypeSchemaId);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.ProjectPatchDto)
            {
                var dto = (API.Dtos.ProjectPatchDto)from;
                var entity = _DBContext.Project.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(Project), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (Project)to;

            if (from is Data.Entities.Project)
            {
                var entity = (Data.Entities.Project)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.BacklogItemTypeSchemaID = entity.BacklogItemTypeSchemaID;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is API.Dtos.ProjectPostDto)
            {
                var dto = (API.Dtos.ProjectPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.BacklogItemTypeSchemaID = dto.BacklogItemTypeSchemaId;
            }
            else if (from is API.Dtos.ProjectPatchDto)
            {
                var dto = (API.Dtos.ProjectPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
