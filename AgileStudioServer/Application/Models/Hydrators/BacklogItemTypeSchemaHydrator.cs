
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemTypeSchemaHydrator : AbstractModelHydrator
    {
        private BacklogItemTypeSchemaService _backlogItemTypeSchemaService;
        private UserHydrator _userHydrator;

        public BacklogItemTypeSchemaHydrator(
            BacklogItemTypeSchemaService backlogItemTypeSchemaService,
            UserHydrator userHydrator)
        {
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
            _userHydrator = userHydrator;
        }

        public BacklogItemTypeSchema Hydrate(Data.Entities.BacklogItemTypeSchema entity, BacklogItemTypeSchema? model = null)
        {
            if (model == null)
            {
                if (entity.ID > 0)
                {
                    model = _backlogItemTypeSchemaService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(BacklogItemTypeSchema), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new BacklogItemTypeSchema(entity.Title);
                }
            }

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            return model;
        }

        public BacklogItemTypeSchema Hydrate(API.DtosNew.BacklogItemTypeSchemaPostDto dto, BacklogItemTypeSchema? model = null)
        {
            model ??= new BacklogItemTypeSchema(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public BacklogItemTypeSchema Hydrate(API.DtosNew.BacklogItemTypeSchemaPatchDto dto, BacklogItemTypeSchema? model = null)
        {
            model ??= _backlogItemTypeSchemaService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeSchema), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
