using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class ReleaseDataProvider
    {
        private readonly DBContext _DBContext;

        public ReleaseDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<ReleaseApiResource> ListForProjectId(int projectId)
        {
            List<Release> releases = _DBContext.Release.Where(release => release.Project.ID == projectId).ToList();

            List<ReleaseApiResource> releaseApiResources = new();
            releases.ForEach(release =>
            {
                LoadReferences(release);

                releaseApiResources.Add(
                    new ReleaseApiResource(release)
                );
            });

            return releaseApiResources;
        }

        public virtual ReleaseApiResource? Get(int id)
        {
            Release? release = _DBContext.Release.Find(id);
            if (release is null)
            {
                return null;
            }

            LoadReferences(release);

            return new ReleaseApiResource(release);
        }

        public virtual ReleaseApiResource Create(ReleasePostDto releasePostDto)
        {
            var project = _DBContext.Project.Find(releasePostDto.ProjectId) ??
                throw new EntityNotFoundException(nameof(Project), releasePostDto.ProjectId.ToString());

            var release = new Release(releasePostDto.Title)
            {
                Project = project,
                Description = releasePostDto.Description,
                StartDate = releasePostDto.StartDate,
                EndDate = releasePostDto.EndDate
            };

            _DBContext.Add(release);
            _DBContext.SaveChanges();

            return new ReleaseApiResource(release);
        }

        public virtual ReleaseApiResource Update(int id, ReleasePatchDto releasePatchDto)
        {
            var release = _DBContext.Release.Find(id) ??
                throw new EntityNotFoundException(nameof(Release), id.ToString());

            release.Title = releasePatchDto.Title;
            release.Description = releasePatchDto.Description;
            release.StartDate = releasePatchDto.StartDate;
            release.EndDate = releasePatchDto.EndDate;
            _DBContext.SaveChanges();

            LoadReferences(release);

            return new ReleaseApiResource(release);
        }

        public virtual void Delete(int id)
        {
            var release = _DBContext.Release.Find(id) ??
                throw new EntityNotFoundException(nameof(Release), id.ToString());

            _DBContext.Release.Remove(release);
            _DBContext.SaveChanges();
        }

        private void LoadReferences(Release release)
        {
            _DBContext.Entry(release).Reference("Project").Load();
            _DBContext.Entry(release).Reference("CreatedBy").Load();
        }
    }
}
