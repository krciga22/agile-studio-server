using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class UserServiceTest : AbstractServiceTest
    {
        private readonly UserService _userService;

        public UserServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            UserService userService) : base(dbContext, fixtures)
        {
            _userService = userService;
        }

        [Fact]
        public void Create_ReturnsUser()
        {
            User user = new("test@test.com", "Test", "User");

            user = _userService.Create(user);

            Assert.NotNull(user);
            Assert.True(user.ID > 0);
        }

        [Fact]
        public void Get_ReturnsUser()
        {
            var user = _Fixtures.CreateUser();

            var returnedUser = _userService.Get(user.ID);

            Assert.NotNull(returnedUser);
            Assert.Equal(user.ID, returnedUser.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedUser()
        {
            var user = _Fixtures.CreateUser();
            var email = $"test2@test.com";

            user.Email = email;
            user = _userService.Update(user);

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void Delete_DeletesUser()
        {
            var user = _Fixtures.CreateUser();

            _userService.Delete(user);

            user = _userService.Get(user.ID);
            Assert.Null(user);
        }
    }
}
