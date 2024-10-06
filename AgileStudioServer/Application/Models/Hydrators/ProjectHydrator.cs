
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Core.Hydrator;
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
                || from == typeof(API.DtosNew.ProjectPostDto)
                || from == typeof(API.DtosNew.ProjectPatchDto)
            ) && to == typeof(Project);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? model = null;

            if (to != typeof(Project))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Data.Entities.Project)
            {
                var entity = (Data.Entities.Project)from;
                model = new Project(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.DtosNew.ProjectPostDto)
            {
                var dto = (API.DtosNew.ProjectPostDto)from;
                model = new Project(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.DtosNew.ProjectPatchDto)
            {
                var dto = (API.DtosNew.ProjectPatchDto)from;
                var entity = _DBContext.Project.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(Project), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (to is not Project)
            {
                throw new Exception("Unsupported to");
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
            else if (from is API.DtosNew.ProjectPostDto)
            {
                var dto = (API.DtosNew.ProjectPostDto)from;
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
            else if (from is API.DtosNew.ProjectPatchDto)
            {
                var dto = (API.DtosNew.ProjectPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
