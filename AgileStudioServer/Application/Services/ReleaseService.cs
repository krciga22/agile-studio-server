using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class ReleaseService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public ReleaseService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<Release> GetByProjectId(int projectId)
        {
            List<Data.Entities.Release> entities = _DBContext.Release.Where(release => 
                release.Project.ID == projectId).ToList();

            return HydrateReleaseModels(entities);
        }

        public virtual Release? Get(int id)
        {
            Data.Entities.Release? entity = _DBContext.Release.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateReleaseModel(entity);
        }

        public virtual Release Create(Release release)
        {
            Data.Entities.Release entity = HydrateReleaseEntity(release);

            _DBContext.Release.Add(entity);
            _DBContext.SaveChanges();

            return HydrateReleaseModel(entity);
        }

        public virtual Release Update(Release release)
        {
            Data.Entities.Release entity = HydrateReleaseEntity(release);

            _DBContext.Release.Update(entity);
            _DBContext.SaveChanges();

            return HydrateReleaseModel(entity);
        }

        public virtual void Delete(Release release)
        {
            Data.Entities.Release entity = HydrateReleaseEntity(release);

            _DBContext.Release.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<Release> HydrateReleaseModels(List<Data.Entities.Release> entities, int depth = 3)
        {
            List<Release> models = new();

            entities.ForEach(entity => {
                Release model = HydrateReleaseModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private Release HydrateReleaseModel(Data.Entities.Release release, int depth = 3)
        {
            return (Release)_Hydrator.Hydrate(
                release, typeof(Release), depth
            );
        }

        private Data.Entities.Release HydrateReleaseEntity(Release release, int depth = 3)
        {
            return (Data.Entities.Release)_Hydrator.Hydrate(
                release, typeof(Data.Entities.Release), depth
            );
        }
    }
}
