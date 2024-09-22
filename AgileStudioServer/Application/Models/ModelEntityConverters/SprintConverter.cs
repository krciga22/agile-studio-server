
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class SprintConverter : AbstractModelEntityConverter, IModelEntityConverter<Sprint, Data.Entities.Sprint>
    {
        private ProjectConverter _projectConverter;
        private UserConverter _userConverter;

        public SprintConverter(
            DBContext dBContext,
            ProjectConverter projectConverter,
            UserConverter userConverter) : base(dBContext)
        {
            _projectConverter = projectConverter;
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Sprint) && entity == typeof(Data.Entities.Sprint);
        }

        public Data.Entities.Sprint ConvertToEntity(Sprint model)
        {
            Data.Entities.Sprint? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.Sprint.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.Sprint),
                        model.ID.ToString()
                    );
                }

                entity.SprintNumber = model.SprintNumber;
            }
            else
            {
                entity = new Data.Entities.Sprint(model.SprintNumber);
            }

            if (model.Project != null) {
                entity.Project = _projectConverter.ConvertToEntity(model.Project);
            }

            if(model.CreatedBy != null){
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Sprint ConvertToModel(Data.Entities.Sprint entity)
        {
            var model = new Sprint(entity.SprintNumber)
            {
                ID = entity.ID
            };

            if (entity.Project != null)
            {
                model.Project = _projectConverter.ConvertToModel(entity.Project);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userConverter.ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
