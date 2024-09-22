
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class UserConverter : IModelEntityConverter<User, Data.Entities.User>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(User) && 
                entity == typeof(Data.Entities.User);
        }

        public Data.Entities.User ConvertToEntity(User model)
        {
            var entity = new Data.Entities.User(model.Email, model.FirstName, model.LastName);
            return entity;
        }

        public User ConvertToModel(Data.Entities.User entity)
        {
            var model = new User(entity.Email, entity.FirstName, entity.LastName);
            return model;
        }
    }
}
