using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class UserService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public UserService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual User? Get(int id)
        {
            Data.Entities.User? entity = _DBContext.User.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateUserModel(entity);
        }

        public virtual User Create(User user)
        {
            Data.Entities.User entity = HydrateUserEntity(user);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateUserModel(entity);
        }

        public virtual User Update(User user)
        {
            Data.Entities.User entity = HydrateUserEntity(user);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateUserModel(entity);
        }

        public virtual void Delete(User user)
        {
            Data.Entities.User entity = HydrateUserEntity(user);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<User> HydrateUserModels(List<Data.Entities.User> entities, int depth = 1)
        {
            List<User> models = new();

            entities.ForEach(entity => {
                User model = HydrateUserModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private User HydrateUserModel(Data.Entities.User user, int depth = 1)
        {
            return (User)_Hydrator.Hydrate(
                user, typeof(User), depth
            );
        }

        private Data.Entities.User HydrateUserEntity(User user, int depth = 1)
        {
            return (Data.Entities.User)_Hydrator.Hydrate(
                user, typeof(Data.Entities.User), depth
            );
        }
    }
}
