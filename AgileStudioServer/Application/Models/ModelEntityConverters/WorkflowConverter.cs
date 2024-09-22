
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class WorkflowConverter : AbstractModelEntityConverter, IModelEntityConverter<Workflow, Data.Entities.Workflow>
    {
        private UserConverter _userConverter;

        public WorkflowConverter(
            DBContext dBContext,
            UserConverter userConverter) : base(dBContext)
        {
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Workflow) && entity == typeof(Data.Entities.Workflow);
        }

        public Data.Entities.Workflow ConvertToEntity(Workflow model)
        {
            Data.Entities.Workflow? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.Workflow.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.Workflow),
                        model.ID.ToString()
                    );
                }

                entity.Title = model.Title;
            }
            else
            {
                entity = new Data.Entities.Workflow(model.Title);
            }

            if (model.CreatedBy != null){
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Workflow ConvertToModel(Data.Entities.Workflow entity)
        {
            var model = new Workflow(entity.Title)
            {
                ID = entity.ID
            };

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userConverter.ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
