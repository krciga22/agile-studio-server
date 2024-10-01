
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowHydrator : AbstractModelHydrator
    {
        public WorkflowHydrator(
            DBContext dbContext, 
            HydratorRegistry hydratorRegistry
        ) : base(dbContext, hydratorRegistry)
        {
            hydratorRegistry.Register(this);
        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.Workflow)
                || from == typeof(API.DtosNew.WorkflowPostDto)
                || from == typeof(API.DtosNew.WorkflowPatchDto)
            ) && to == typeof(Workflow);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth)
        {
            Object? model = null;

            if (to != typeof(Workflow))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Data.Entities.Workflow)
            {
                var entity = (Data.Entities.Workflow)from;
                model = new Workflow(entity.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.WorkflowPostDto)
            {
                var dto = (API.DtosNew.WorkflowPostDto)from;
                model = new Workflow(dto.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.WorkflowPatchDto)
            {
                var dto = (API.DtosNew.WorkflowPatchDto)from;
                var entity = _DBContext.Workflow.Find(dto.ID);
                if(entity != null)
                {
                    model = Hydrate(entity, typeof(Workflow), maxDepth, depth);
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
            if(to is not Workflow)
            {
                throw new Exception("Unsupported to");
            }

            var model = (Workflow)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.Workflow)
            {
                var entity = (Data.Entities.Workflow) from;
                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if(nextDepth <= maxDepth)
                {
                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)_HydratorRegistry.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if(from is API.DtosNew.WorkflowPostDto)
            {
                var dto = (API.DtosNew.WorkflowPostDto) from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
            else if(from is API.DtosNew.WorkflowPatchDto)
            {
                var dto = (API.DtosNew.WorkflowPatchDto) from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
