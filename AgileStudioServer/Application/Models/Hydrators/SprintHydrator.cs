﻿
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class SprintHydrator : AbstractModelHydrator
    {
        public SprintHydrator(
            DBContext dbContext,
            HydratorRegistry hydratorRegistry
        ) : base(dbContext, hydratorRegistry)
        {
            hydratorRegistry.Register(this);
        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.Sprint)
                || from == typeof(API.DtosNew.SprintPostDto)
                || from == typeof(API.DtosNew.SprintPatchDto)
            ) && to == typeof(Sprint);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth)
        {
            Object? model = null;

            if (to != typeof(Sprint))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Data.Entities.Sprint)
            {
                var entity = (Data.Entities.Sprint)from;
                model = new Sprint(entity.SprintNumber);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.SprintPostDto)
            {
                model = new Sprint(0); // todo fix hard coded sprint number
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.SprintPatchDto)
            {
                var dto = (API.DtosNew.SprintPatchDto)from;
                var entity = _DBContext.Sprint.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(Sprint), maxDepth, depth);
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
            if (to is not Sprint)
            {
                throw new Exception("Unsupported to");
            }

            var model = (Sprint)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.Sprint)
            {
                var entity = (Data.Entities.Sprint)from;

                model.ID = entity.ID;
                model.SprintNumber = entity.SprintNumber;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.StartDate = entity.StartDate;
                model.EndDate = entity.EndDate;

                if (nextDepth <= maxDepth)
                {
                    model.Project = (Project)_HydratorRegistry.Hydrate(
                        entity.Project, typeof(Project), maxDepth, nextDepth
                    );

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)_HydratorRegistry.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.DtosNew.SprintPostDto)
            {
                var dto = (API.DtosNew.SprintPostDto)from;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;

                if (depth < maxDepth)
                {
                    Data.Entities.Project? projectEntity = _DBContext.Project.Find(dto.ProjectId) ??
                        throw new ModelNotFoundException(
                            nameof(Project),
                            dto.ProjectId.ToString()
                        );

                    model.Project = (Project)_HydratorRegistry.Hydrate(
                        projectEntity, typeof(Project), maxDepth, nextDepth
                    );
                }
            }
            else if (from is API.DtosNew.SprintPatchDto)
            {
                var dto = (API.DtosNew.SprintPatchDto)from;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
            }
        }
    }
}
