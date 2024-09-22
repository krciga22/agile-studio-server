
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class UserConverter : AbstractModelEntityConverter, IModelEntityConverter<User, Data.Entities.User>
    {
        public UserConverter(DBContext dBContext) : base(dBContext)
        {
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(User) && 
                entity == typeof(Data.Entities.User);
        }

        public Data.Entities.User ConvertToEntity(User model)
        {
            Data.Entities.User? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.User.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.User),
                        model.ID.ToString()
                    );
                }

                entity.Email = model.Email;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
            }
            else
            {
                entity = new Data.Entities.User(model.Email, model.FirstName, model.LastName);
            }

            return entity;
        }

        public User ConvertToModel(Data.Entities.User entity)
        {
            var model = new User(entity.Email, entity.FirstName, entity.LastName)
            {
                ID = entity.ID
            };

            return model;
        }
    }
}
