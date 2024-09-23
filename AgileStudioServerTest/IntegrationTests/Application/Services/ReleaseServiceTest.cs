using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class ReleaseRepositoryTest : AbstractServiceTest
    {
        private readonly ReleaseService _releaseService;

        public ReleaseRepositoryTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            ReleaseService releaseService) : base(dbContext, fixtures)
        {
            _releaseService = releaseService;
        }

        [Fact]
        public void Create_ReturnsRelease()
        {
            Release release = new("Test Release");
            release.Project = _Fixtures.CreateProject();

            release = _releaseService.Create(release);

            Assert.NotNull(release);
            Assert.True(release.ID > 0);
        }

        [Fact]
        public void Get_ReturnsRelease()
        {
            var release = _Fixtures.CreateRelease();

            var returnedRelease = _releaseService.Get(release.ID);

            Assert.NotNull(returnedRelease);
            Assert.Equal(release.ID, returnedRelease.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllReleases()
        {
            var project = _Fixtures.CreateProject();
            var releases = new List<Release>
            {
                _Fixtures.CreateRelease("Test Release 1", project),
                _Fixtures.CreateRelease("Test Release 2", project)
            };

            List<Release> returnedReleases = _releaseService.GetByProjectId(project.ID);

            Assert.Equal(releases.Count, returnedReleases.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedRelease()
        {
            var release = _Fixtures.CreateRelease();
            var title = $"{release.Title} Updated";

            release.Title = title;
            release = _releaseService.Update(release);

            Assert.NotNull(release);
            Assert.Equal(title, release.Title);
        }

        [Fact]
        public void Delete_DeletesRelease()
        {
            var release = _Fixtures.CreateRelease();

            _releaseService.Delete(release);

            release = _releaseService.Get(release.ID);
            Assert.Null(release);
        }
    }
}
