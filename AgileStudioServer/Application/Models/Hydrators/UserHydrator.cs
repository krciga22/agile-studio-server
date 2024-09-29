
namespace AgileStudioServer.Application.Models.Hydrators
{
    public class UserHydrator : AbstractModelHydrator
    {
        public User Hydrate(Data.Entities.User entity, User? model = null)
        {
            if(model == null)
            {
                model = new User(entity.Email, entity.FirstName, entity.LastName);
            }

            model.Email = entity.Email;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.CreatedOn = entity.CreatedOn;

            return model;
        }
    }
}
