
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class BacklogItemConverter : AbstractModelEntityConverter, IModelEntityConverter<BacklogItem, Data.Entities.BacklogItem>
    {
        private ProjectConverter _projectConverter;
        private SprintConverter _sprintConverter;
        private ReleaseConverter _releaseConverter;
        private BacklogItemTypeConverter _backlogItemTypeConverter;
        private WorkflowStateConverter _workflowStateConverter;
        private UserConverter _userConverter;

        public BacklogItemConverter(
            DBContext dBContext,
            ProjectConverter projectConverter,
            SprintConverter sprintConverter,
            ReleaseConverter releaseConverter,
            BacklogItemTypeConverter backlogItemTypeConverter,
            WorkflowStateConverter workflowStateConverter,
            UserConverter userConverter) : base(dBContext)
        {
            _projectConverter = projectConverter;
            _sprintConverter = sprintConverter;
            _releaseConverter = releaseConverter;
            _backlogItemTypeConverter = backlogItemTypeConverter;
            _workflowStateConverter = workflowStateConverter;
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(BacklogItem) && 
                entity == typeof(Data.Entities.BacklogItem);
        }

        public Data.Entities.BacklogItem ConvertToEntity(BacklogItem model)
        {
            Data.Entities.BacklogItem? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.BacklogItem.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.BacklogItem), 
                        model.ID.ToString()
                    );
                }

                entity.Title = model.Title;
            }
            else
            {
                entity = new Data.Entities.BacklogItem(model.Title);
            }

            if (model.Project != null)
            {
                entity.Project = _projectConverter.ConvertToEntity(model.Project);
            }

            if (model.Sprint != null)
            {
                entity.Sprint = _sprintConverter.ConvertToEntity(model.Sprint);
            }

            if (model.Release != null)
            {
                entity.Release = _releaseConverter.ConvertToEntity(model.Release);
            }

            if (model.BacklogItemType != null)
            {
                entity.BacklogItemType = _backlogItemTypeConverter
                    .ConvertToEntity(model.BacklogItemType);
            }

            if (model.WorkflowState != null)
            {
                entity.WorkflowState = _workflowStateConverter
                    .ConvertToEntity(model.WorkflowState);
            }

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public BacklogItem ConvertToModel(Data.Entities.BacklogItem entity)
        {
            var model = new BacklogItem(entity.Title)
            {
                ID = entity.ID
            };

            if (entity.Project != null)
            {
                model.Project = _projectConverter.ConvertToModel(entity.Project);
            }

            if (entity.Sprint != null)
            {
                model.Sprint = _sprintConverter.ConvertToModel(entity.Sprint);
            }

            if (entity.Release != null)
            {
                model.Release = _releaseConverter.ConvertToModel(entity.Release);
            }

            if (entity.BacklogItemType != null)
            {
                model.BacklogItemType = _backlogItemTypeConverter
                    .ConvertToModel(entity.BacklogItemType);
            }

            if (entity.WorkflowState != null)
            {
                model.WorkflowState = _workflowStateConverter
                    .ConvertToModel(entity.WorkflowState);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userConverter.ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
