
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemTypeHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.BacklogItemType)
                || from == typeof(API.Dtos.BacklogItemTypePostDto)
                || from == typeof(API.Dtos.BacklogItemTypePatchDto)
            ) && to == typeof(BacklogItemType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? model = null;

            if (from is Data.Entities.BacklogItemType)
            {
                var entity = (Data.Entities.BacklogItemType)from;
                model = new BacklogItemType(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemTypePostDto)
            {
                var dto = (API.Dtos.BacklogItemTypePostDto)from;
                model = new BacklogItemType(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemTypePatchDto)
            {
                var dto = (API.Dtos.BacklogItemTypePatchDto)from;
                var entity = _DBContext.BacklogItemType.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItemType), maxDepth, depth, referenceHydrator);
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
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupported(from.GetType(), to.GetType());
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

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    model.BacklogItemTypeSchema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        entity.BacklogItemTypeSchema, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    model.Workflow = (Workflow)referenceHydrator.Hydrate(
                        entity.Workflow, typeof(Workflow), maxDepth, nextDepth
                    );

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)referenceHydrator.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.BacklogItemTypePostDto)
            {
                var dto = (API.Dtos.BacklogItemTypePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                if(referenceHydrator != null && nextDepth <= maxDepth)
                {
                    Data.Entities.BacklogItemTypeSchema? backlogItemTypeSchemaEntity =
                    _DBContext.BacklogItemTypeSchema.Find(dto.BacklogItemTypeSchemaId) ??
                        throw new ModelNotFoundException(
                            nameof(BacklogItemTypeSchema),
                            dto.BacklogItemTypeSchemaId.ToString()
                        );

                    model.BacklogItemTypeSchema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        backlogItemTypeSchemaEntity, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    Data.Entities.Workflow? workflowEntity =
                        _DBContext.Workflow.Find(dto.WorkflowId) ??
                            throw new ModelNotFoundException(
                                nameof(Workflow),
                                dto.WorkflowId.ToString()
                            );

                    model.Workflow = (Workflow)referenceHydrator.Hydrate(
                        workflowEntity, typeof(Workflow), maxDepth, nextDepth
                    );
                }
            }
            else if (from is API.Dtos.BacklogItemTypePatchDto)
            {
                var dto = (API.Dtos.BacklogItemTypePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
