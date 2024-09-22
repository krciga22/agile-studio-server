
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class ProjectConverter : AbstractModelEntityConverter, IModelEntityConverter<Project, Data.Entities.Project>
    {
        private BacklogItemTypeSchemaConverter _backlogItemTypeSchemaConverter;
        private UserConverter _userConverter;

        public ProjectConverter(
            DBContext dBContext,
            BacklogItemTypeSchemaConverter backlogItemTypeSchemaConverter,
            UserConverter userConverter) : base(dBContext)
        {
            _backlogItemTypeSchemaConverter = backlogItemTypeSchemaConverter;
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Project) && entity == typeof(Data.Entities.Project);
        }

        public Data.Entities.Project ConvertToEntity(Project model)
        {
            Data.Entities.Project? entity = null;

            if (model.ID > 0) {
                entity = _DBContext.Project.Find(model.ID);
                if(entity == null) {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.Project), 
                        model.ID.ToString()
                    );
                }

                entity.Title = model.Title;
            }
            else
            {
                entity = new Data.Entities.Project(model.Title);
            }

            if (model.BacklogItemTypeSchema != null) {
                entity.BacklogItemTypeSchema = _backlogItemTypeSchemaConverter
                    .ConvertToEntity(model.BacklogItemTypeSchema);
            }

            if(model.CreatedBy != null){
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Project ConvertToModel(Data.Entities.Project entity)
        {
            var model = new Project(entity.Title)
            {
                ID = entity.ID
            };

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = _backlogItemTypeSchemaConverter
                    .ConvertToModel(entity.BacklogItemTypeSchema);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userConverter.ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
