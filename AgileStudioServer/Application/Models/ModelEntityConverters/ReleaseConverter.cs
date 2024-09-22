
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class ReleaseConverter : AbstractModelEntityConverter, IModelEntityConverter<Release, Data.Entities.Release>
    {
        private ProjectConverter _projectConverter;
        private UserConverter _userConverter;

        public ReleaseConverter(
            DBContext dBContext,
            ProjectConverter projectConverter,
            UserConverter userConverter) : base(dBContext)
        {
            _projectConverter = projectConverter;
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Release) && entity == typeof(Data.Entities.Release);
        }

        public Data.Entities.Release ConvertToEntity(Release model)
        {
            Data.Entities.Release? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.Release.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.Release),
                        model.ID.ToString()
                    );
                }

                entity.Title = model.Title;
            }
            else
            {
                entity = new Data.Entities.Release(model.Title);
            }

            if (model.Project != null) {
                entity.Project = _projectConverter.ConvertToEntity(model.Project);
            }

            if(model.CreatedBy != null){
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Release ConvertToModel(Data.Entities.Release entity)
        {
            var model = new Release(entity.Title)
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
