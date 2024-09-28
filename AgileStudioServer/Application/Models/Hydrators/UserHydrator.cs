
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class UserHydrator : AbstractModelHydrator
    {
        private UserService _userService;

        public UserHydrator(UserService userService)
        {
            _userService = userService;
        }

        public User Hydrate(Data.Entities.User entity, User? model = null)
        {
            if(model == null)
            {
                if(entity.ID > 0)
                {
                    model = _userService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(User), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new User(entity.Email, entity.FirstName, entity.LastName);
                }
            }

            model.Email = entity.Email;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.CreatedOn = entity.CreatedOn;

            return model;
        }
    }
}
