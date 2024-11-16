
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemHydrator : AbstractModelHydrator
    {
        public BacklogItemHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Data.Entities.BacklogItem) || 
                from == typeof(API.Dtos.BacklogItemPostDto) || 
                from == typeof(API.Dtos.BacklogItemPatchDto)
            ) && to == typeof(BacklogItem);
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
                var backlogItem = _DBContext.BacklogItem.Find(from);
                if (backlogItem != null)
                {
                    from = backlogItem;
                }
            }

            if (from is Data.Entities.BacklogItem)
            {
                var entity = (Data.Entities.BacklogItem)from;
                model = new BacklogItem(
                    entity.Title, 
                    entity.ProjectID, 
                    entity.BacklogItemTypeID, 
                    entity.WorkflowStateID
                );
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemPostDto)
            {
                var dto = (API.Dtos.BacklogItemPostDto)from;
                model = new BacklogItem(
                    dto.Title,
                    dto.ProjectId,
                    dto.BacklogItemTypeId,
                    dto.WorkflowStateId
                );
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemPatchDto)
            {
                var dto = (API.Dtos.BacklogItemPatchDto)from;
                var entity = _DBContext.BacklogItem.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItem), maxDepth, depth, referenceHydrator);
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

            var model = (BacklogItem)to;

            if (from is Data.Entities.BacklogItem)
            {
                var entity = (Data.Entities.BacklogItem)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.ProjectID = entity.ProjectID;
                model.BacklogItemTypeID = entity.BacklogItemTypeID;
                model.WorkflowStateID = entity.WorkflowStateID;
                model.SprintID = entity.SprintID;
                model.ReleaseID = entity.ReleaseID;
                model.CreatedByID = entity.CreatedByID;
                model.ParentBacklogItemId = entity.ParentBacklogItemId;
            }
            else if (from is API.Dtos.BacklogItemPostDto)
            {
                var dto = (API.Dtos.BacklogItemPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.ProjectID = dto.ProjectId;
                model.BacklogItemTypeID = dto.BacklogItemTypeId;
                model.WorkflowStateID = dto.WorkflowStateId;
                model.SprintID = dto.SprintId;
                model.ReleaseID = dto.ReleaseId;
            }
            else if (from is API.Dtos.BacklogItemPatchDto)
            {
                var dto = (API.Dtos.BacklogItemPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.WorkflowStateID = dto.WorkflowStateId;
                model.SprintID = dto.SprintId;
                model.ReleaseID = dto.ReleaseId;
            }
        }
    }
}
