
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ReleaseHydrator : AbstractModelHydrator
    {
        public ReleaseHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.Release)
                || from == typeof(API.Dtos.ReleasePostDto)
                || from == typeof(API.Dtos.ReleasePatchDto)
            ) && to == typeof(Release);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? model = null;

            if (from is Data.Entities.Release)
            {
                var entity = (Data.Entities.Release)from;
                model = new Release(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.ReleasePostDto)
            {
                var dto = (API.Dtos.ReleasePostDto)from;
                model = new Release(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.ReleasePatchDto)
            {
                var dto = (API.Dtos.ReleasePatchDto)from;
                var entity = _DBContext.Release.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(Release), maxDepth, depth, referenceHydrator);
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

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    model.Project = (Project)referenceHydrator.Hydrate(
                        entity.Project, typeof(Project), maxDepth, nextDepth
                    );

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)referenceHydrator.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.ReleasePostDto)
            {
                var dto = (API.Dtos.ReleasePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;

                if (referenceHydrator != null && depth < maxDepth)
                {
                    Data.Entities.Project? projectEntity = _DBContext.Project.Find(dto.ProjectId) ??
                        throw new ModelNotFoundException(
                            nameof(Project),
                            dto.ProjectId.ToString()
                        );

                    model.Project = (Project)referenceHydrator.Hydrate(
                        projectEntity, typeof(Project), maxDepth, nextDepth
                    );
                }
            }
            else if (from is API.Dtos.ReleasePatchDto)
            {
                var dto = (API.Dtos.ReleasePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
            }
        }
    }
}
