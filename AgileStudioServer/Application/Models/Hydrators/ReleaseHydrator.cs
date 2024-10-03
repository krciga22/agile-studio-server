﻿
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ReleaseHydrator : AbstractModelHydrator
    {
        public ReleaseHydrator(
            DBContext dbContext,
            HydratorRegistry hydratorRegistry
        ) : base(dbContext, hydratorRegistry)
        {
            hydratorRegistry.Register(this);
        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.Release)
                || from == typeof(API.DtosNew.ReleasePostDto)
                || from == typeof(API.DtosNew.ReleasePatchDto)
            ) && to == typeof(Release);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth)
        {
            Object? model = null;

            if (to != typeof(Release))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Data.Entities.Release)
            {
                var entity = (Data.Entities.Release)from;
                model = new Release(entity.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.ReleasePostDto)
            {
                var dto = (API.DtosNew.ReleasePostDto)from;
                model = new Release(dto.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.ReleasePatchDto)
            {
                var dto = (API.DtosNew.ReleasePatchDto)from;
                var entity = _DBContext.Release.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(Release), maxDepth, depth);
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
            if (to is not Release)
            {
                throw new Exception("Unsupported to");
            }

            var model = (Release)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.Release)
            {
                var entity = (Data.Entities.Release)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
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
            else if (from is API.DtosNew.ReleasePostDto)
            {
                var dto = (API.DtosNew.ReleasePostDto)from;
                model.Title = dto.Title;
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
            else if (from is API.DtosNew.ReleasePatchDto)
            {
                var dto = (API.DtosNew.ReleasePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
            }
        }
    }
}