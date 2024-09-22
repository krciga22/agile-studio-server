using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class ReleaseService
    {
        private readonly DBContext _DBContext;

        private readonly ReleaseConverter _converter;

        public ReleaseService(DBContext dbContext, ReleaseConverter releaseConverter)
        {
            _DBContext = dbContext;
            _converter = releaseConverter;
        }

        public virtual List<Release> GetByProjectId(int projectId)
        {
            List<Data.Entities.Release> entities = _DBContext.Release.Where(release => 
                release.Project.ID == projectId).ToList();

            List<Release> models = new();
            entities.ForEach(entity =>
            {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual Release? Get(int id)
        {
            Data.Entities.Release? entity = _DBContext.Release.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual Release Create(Release release)
        {
            Data.Entities.Release entity = _converter.ConvertToEntity(release);

            _DBContext.Add(release);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual Release Update(Release release)
        {
            Data.Entities.Release entity = _converter.ConvertToEntity(release);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(Release release)
        {
            Data.Entities.Release entity = _converter.ConvertToEntity(release);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
