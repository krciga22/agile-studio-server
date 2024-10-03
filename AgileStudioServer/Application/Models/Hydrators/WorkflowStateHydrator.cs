﻿
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowStateHydrator : AbstractModelHydrator
    {
        public WorkflowStateHydrator(
            DBContext dbContext,
            HydratorRegistry hydratorRegistry
        ) : base(dbContext, hydratorRegistry)
        {
            hydratorRegistry.Register(this);
        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.WorkflowState)
                || from == typeof(API.DtosNew.WorkflowStatePostDto)
                || from == typeof(API.DtosNew.WorkflowStatePatchDto)
            ) && to == typeof(WorkflowState);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth)
        {
            Object? model = null;

            if (to != typeof(WorkflowState))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Data.Entities.WorkflowState)
            {
                var entity = (Data.Entities.WorkflowState)from;
                model = new WorkflowState(entity.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.WorkflowStatePostDto)
            {
                var dto = (API.DtosNew.WorkflowStatePostDto)from;
                model = new WorkflowState(dto.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.WorkflowStatePatchDto)
            {
                var dto = (API.DtosNew.WorkflowStatePatchDto)from;
                var entity = _DBContext.WorkflowState.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(WorkflowState), maxDepth, depth);
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
            if (to is not WorkflowState)
            {
                throw new Exception("Unsupported to");
            }

            var model = (WorkflowState)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.WorkflowState)
            {
                var entity = (Data.Entities.WorkflowState)from;
                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if (nextDepth <= maxDepth)
                {
                    if (entity.Workflow != null)
                    {
                        model.Workflow = (Workflow)_HydratorRegistry.Hydrate(
                            entity.Workflow, typeof(Workflow), maxDepth, nextDepth
                        );
                    }

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)_HydratorRegistry.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.DtosNew.WorkflowStatePostDto)
            {
                var dto = (API.DtosNew.WorkflowStatePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                if (depth < maxDepth)
                {
                    if (dto.WorkflowId > 0)
                    {
                        Data.Entities.Workflow? workflowEntity = _DBContext.Workflow.Find(dto.WorkflowId) ??
                            throw new ModelNotFoundException(
                                nameof(Workflow), 
                                dto.WorkflowId.ToString()
                            );

                        model.Workflow = (Workflow)_HydratorRegistry.Hydrate(
                            workflowEntity, typeof(Workflow), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.DtosNew.WorkflowStatePatchDto)
            {
                var dto = (API.DtosNew.WorkflowStatePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}