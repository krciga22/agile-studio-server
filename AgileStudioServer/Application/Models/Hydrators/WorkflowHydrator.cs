﻿
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowHydrator : AbstractModelHydrator
    {
        public WorkflowHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Data.Entities.Workflow) || 
                from == typeof(API.Dtos.WorkflowPostDto) || 
                from == typeof(API.Dtos.WorkflowPatchDto)
            ) && to == typeof(Workflow);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? model = null;

            if(from is int)
            {
                var workflow = _DBContext.Workflow.Find(from);
                if(workflow != null)
                {
                    from = workflow;
                }
            }

            if (from is Data.Entities.Workflow)
            {
                var entity = (Data.Entities.Workflow)from;
                model = new Workflow(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.WorkflowPostDto)
            {
                var dto = (API.Dtos.WorkflowPostDto)from;
                model = new Workflow(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.WorkflowPatchDto)
            {
                var dto = (API.Dtos.WorkflowPatchDto)from;
                var entity = _DBContext.Workflow.Find(dto.ID);
                if(entity != null)
                {
                    model = Hydrate(entity, typeof(Workflow), maxDepth, depth, referenceHydrator);
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

            var model = (Workflow)to;

            if (from is Data.Entities.Workflow)
            {
                var entity = (Data.Entities.Workflow) from;
                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedById = entity.CreatedByID;
            }
            else if(from is API.Dtos.WorkflowPostDto)
            {
                var dto = (API.Dtos.WorkflowPostDto) from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
            else if(from is API.Dtos.WorkflowPatchDto)
            {
                var dto = (API.Dtos.WorkflowPatchDto) from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
