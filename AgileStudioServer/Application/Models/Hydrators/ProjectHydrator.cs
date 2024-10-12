
using AgileStudioServer.Application.Exceptions;
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
                from == typeof(Data.Entities.Project)
                || from == typeof(API.Dtos.ProjectPostDto)
                || from == typeof(API.Dtos.ProjectPatchDto)
            ) && to == typeof(Project);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? model = null;

            if (from is Data.Entities.Project)
            {
                var entity = (Data.Entities.Project)from;
                model = new Project(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.ProjectPostDto)
            {
                var dto = (API.Dtos.ProjectPostDto)from;
                model = new Project(dto.Title);
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
                throw new HydrationNotSupported(from.GetType(), to.GetType());
            }

            var model = (Project)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.Project)
            {
                var entity = (Data.Entities.Project)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    model.BacklogItemTypeSchema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        entity.BacklogItemTypeSchema, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)referenceHydrator.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.ProjectPostDto)
            {
                var dto = (API.Dtos.ProjectPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    Data.Entities.BacklogItemTypeSchema? backlogItemTypeSchemaEntity = _DBContext.BacklogItemTypeSchema.Find(dto.BacklogItemTypeSchemaId) ??
                        throw new ModelNotFoundException(
                            nameof(BacklogItemTypeSchema),
                            dto.BacklogItemTypeSchemaId.ToString()
                        );

                    model.BacklogItemTypeSchema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        backlogItemTypeSchemaEntity, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );
                }
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
