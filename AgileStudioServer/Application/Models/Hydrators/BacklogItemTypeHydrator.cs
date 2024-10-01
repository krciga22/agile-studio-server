
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemTypeHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeHydrator(
            DBContext dbContext,
            HydratorRegistry hydratorRegistry
        ) : base(dbContext, hydratorRegistry)
        {
            hydratorRegistry.Register(this);
        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.BacklogItemType)
                || from == typeof(API.DtosNew.BacklogItemTypePostDto)
                || from == typeof(API.DtosNew.BacklogItemTypePatchDto)
            ) && to == typeof(BacklogItemType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth)
        {
            Object? model = null;

            if (to != typeof(BacklogItemType))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Data.Entities.BacklogItemType)
            {
                var entity = (Data.Entities.BacklogItemType)from;
                model = new BacklogItemType(entity.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.BacklogItemTypePostDto)
            {
                var dto = (API.DtosNew.BacklogItemTypePostDto)from;
                model = new BacklogItemType(dto.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.BacklogItemTypePatchDto)
            {
                var dto = (API.DtosNew.BacklogItemTypePatchDto)from;
                var entity = _DBContext.BacklogItemType.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItemType), maxDepth, depth);
                    Hydrate(dto, model, maxDepth, depth);
                }
            }

            if (model == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth)
        {
            if (to is not BacklogItemType)
            {
                throw new Exception("Unsupported to");
            }

            var model = (BacklogItemType)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.BacklogItemType)
            {
                var entity = (Data.Entities.BacklogItemType)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if (nextDepth <= maxDepth)
                {
                    model.BacklogItemTypeSchema = (BacklogItemTypeSchema)_HydratorRegistry.Hydrate(
                        entity.BacklogItemTypeSchema, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    model.Workflow = (Workflow)_HydratorRegistry.Hydrate(
                        entity.Workflow, typeof(Workflow), maxDepth, nextDepth
                    );

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)_HydratorRegistry.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.DtosNew.BacklogItemTypePostDto)
            {
                var dto = (API.DtosNew.BacklogItemTypePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                Data.Entities.BacklogItemTypeSchema? backlogItemTypeSchemaEntity = 
                    _DBContext.BacklogItemTypeSchema.Find(dto.BacklogItemTypeSchemaId) ??
                        throw new ModelNotFoundException(
                            nameof(BacklogItemTypeSchema),
                            dto.BacklogItemTypeSchemaId.ToString()
                        );

                model.BacklogItemTypeSchema = (BacklogItemTypeSchema)_HydratorRegistry.Hydrate(
                    backlogItemTypeSchemaEntity, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                );

                Data.Entities.Workflow? workflowEntity =
                    _DBContext.Workflow.Find(dto.WorkflowId) ??
                        throw new ModelNotFoundException(
                            nameof(Workflow),
                            dto.WorkflowId.ToString()
                        );

                model.Workflow = (Workflow)_HydratorRegistry.Hydrate(
                    workflowEntity, typeof(Workflow), maxDepth, nextDepth
                );
            }
            else if (from is API.DtosNew.BacklogItemTypePatchDto)
            {
                var dto = (API.DtosNew.BacklogItemTypePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
