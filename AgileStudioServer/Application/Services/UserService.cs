using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class UserService
    {
        private readonly DBContext _DBContext;

        private readonly UserConverter _converter;

        public UserService(DBContext dbContext, UserConverter userConverter)
        {
            _DBContext = dbContext;
            _converter = userConverter;
        }

        public virtual User? Get(int id)
        {
            Data.Entities.User? entity = _DBContext.User.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual User Create(User user)
        {
            Data.Entities.User entity = _converter.ConvertToEntity(user);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual User Update(User user)
        {
            Data.Entities.User entity = _converter.ConvertToEntity(user);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(User user)
        {
            Data.Entities.User entity = _converter.ConvertToEntity(user);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
