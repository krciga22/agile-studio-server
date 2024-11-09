
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
                from == typeof(int) || 
                from == typeof(Data.Entities.Release) || 
                from == typeof(API.Dtos.ReleasePostDto) || 
                from == typeof(API.Dtos.ReleasePatchDto)
            ) && to == typeof(Release);
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
                var release = _DBContext.Release.Find(from);
                if (release != null)
                {
                    from = release;
                }
            }

            if (from is Data.Entities.Release)
            {
                var entity = (Data.Entities.Release)from;
                model = new Release(entity.Title, entity.ProjectID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.ReleasePostDto)
            {
                var dto = (API.Dtos.ReleasePostDto)from;
                model = new Release(dto.Title, dto.ProjectId);
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
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (Release)to;

            if (from is Data.Entities.Release)
            {
                var entity = (Data.Entities.Release)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.StartDate = entity.StartDate;
                model.EndDate = entity.EndDate;
                model.ProjectID = entity.ProjectID;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is API.Dtos.ReleasePostDto)
            {
                var dto = (API.Dtos.ReleasePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
                model.ProjectID = dto.ProjectId;
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
