
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemTypeHydrator : AbstractModelHydrator
    {
        private BacklogItemTypeService _backlogItemTypeService;
        private BacklogItemTypeSchemaService _backlogItemTypeSchemaService;
        private BacklogItemTypeSchemaHydrator _backlogItemTypeSchemaHydrator;
        private UserHydrator _userHydrator;
        private WorkflowService _workflowService;
        private WorkflowHydrator _workflowHydrator;

        public BacklogItemTypeHydrator(
            BacklogItemTypeService backlogItemTypeService,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService,
            BacklogItemTypeSchemaHydrator backlogItemTypeSchemaHydrator,
            UserHydrator userHydrator,
            WorkflowService workflowService,
            WorkflowHydrator workflowHydrator)
        {
            _backlogItemTypeService = backlogItemTypeService;
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
            _backlogItemTypeSchemaHydrator = backlogItemTypeSchemaHydrator;
            _userHydrator = userHydrator;
            _workflowService = workflowService;
            _workflowHydrator = workflowHydrator;
        }

        public BacklogItemType Hydrate(Data.Entities.BacklogItemType entity, BacklogItemType? model = null)
        {
            if (model == null)
            {
                if (entity.ID > 0)
                {
                    model = _backlogItemTypeService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(BacklogItemType), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new BacklogItemType(entity.Title);
                }
            }

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator.Hydrate(entity.BacklogItemTypeSchema);
            }

            if (entity.Workflow != null)
            {
                model.Workflow = _workflowHydrator.Hydrate(entity.Workflow);
            }

            return model;
        }

        public BacklogItemType Hydrate(API.DtosNew.BacklogItemTypePostDto dto, BacklogItemType? model = null)
        {
            model ??= new BacklogItemType(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            model.BacklogItemTypeSchema = _backlogItemTypeSchemaService
                .Get(dto.BacklogItemTypeSchemaId) ?? 
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeSchema), dto.BacklogItemTypeSchemaId.ToString()
                );

            model.Workflow = _workflowService
                .Get(dto.WorkflowId) ??
                throw new ModelNotFoundException(
                    nameof(Workflow), dto.WorkflowId.ToString()
                );

            return model;
        }

        public BacklogItemType Hydrate(API.DtosNew.BacklogItemTypePatchDto dto, BacklogItemType? model = null)
        {
            model ??= _backlogItemTypeService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemType), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
