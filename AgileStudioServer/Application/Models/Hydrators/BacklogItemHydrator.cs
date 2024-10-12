
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemHydrator : AbstractModelHydrator
    {
        public BacklogItemHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.BacklogItem)
                || from == typeof(API.Dtos.BacklogItemPostDto)
                || from == typeof(API.Dtos.BacklogItemPatchDto)
            ) && to == typeof(BacklogItem);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? model = null;

            if (from is Data.Entities.BacklogItem)
            {
                var entity = (Data.Entities.BacklogItem)from;
                model = new BacklogItem(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemPostDto)
            {
                var dto = (API.Dtos.BacklogItemPostDto)from;
                model = new BacklogItem(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemPatchDto)
            {
                var dto = (API.Dtos.BacklogItemPatchDto)from;
                var entity = _DBContext.BacklogItem.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItem), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupported(from.GetType(), to.GetType());
            }

            var model = (BacklogItem)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.BacklogItem)
            {
                var entity = (Data.Entities.BacklogItem)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    model.Project = (Project)referenceHydrator.Hydrate(
                        entity.Project, typeof(Project), maxDepth, nextDepth
                    );

                    model.BacklogItemType = (BacklogItemType)referenceHydrator.Hydrate(
                        entity.BacklogItemType, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    model.WorkflowState = (WorkflowState)referenceHydrator.Hydrate(
                        entity.WorkflowState, typeof(WorkflowState), maxDepth, nextDepth
                    );

                    if (entity.Sprint != null)
                    {
                        model.Sprint = (Sprint)referenceHydrator.Hydrate(
                            entity.Sprint, typeof(Sprint), maxDepth, nextDepth
                        );
                    }

                    if (entity.Release != null)
                    {
                        model.Release = (Release)referenceHydrator.Hydrate(
                            entity.Release, typeof(Release), maxDepth, nextDepth
                        );
                    }

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)referenceHydrator.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.BacklogItemPostDto)
            {
                var dto = (API.Dtos.BacklogItemPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                if(referenceHydrator != null && nextDepth <= maxDepth)
                {
                    Data.Entities.Project? projectEntity =
                    _DBContext.Project.Find(dto.ProjectId) ??
                        throw new ModelNotFoundException(
                            nameof(Project),
                            dto.ProjectId.ToString()
                        );
                    model.Project = (Project)referenceHydrator.Hydrate(
                        projectEntity, typeof(Project), maxDepth, nextDepth
                    );

                    Data.Entities.BacklogItemType? backlogItemTypeEntity =
                        _DBContext.BacklogItemType.Find(dto.BacklogItemTypeId) ??
                            throw new ModelNotFoundException(
                                nameof(BacklogItemType),
                                dto.BacklogItemTypeId.ToString()
                            );
                    model.BacklogItemType = (BacklogItemType)referenceHydrator.Hydrate(
                        backlogItemTypeEntity, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    Data.Entities.WorkflowState? workflowStateEntity =
                        _DBContext.WorkflowState.Find(dto.WorkflowStateId) ??
                            throw new ModelNotFoundException(
                                nameof(WorkflowState),
                                dto.WorkflowStateId.ToString()
                            );
                    model.WorkflowState = (WorkflowState)referenceHydrator.Hydrate(
                        workflowStateEntity, typeof(WorkflowState), maxDepth, nextDepth
                    );

                    if (dto.SprintId != null)
                    {
                        int sprintId = (int)dto.SprintId;
                        Data.Entities.Sprint? sprintEntity =
                            _DBContext.Sprint.Find(sprintId) ??
                                throw new ModelNotFoundException(
                                    nameof(Sprint),
                                    sprintId.ToString()
                                );
                        model.Sprint = (Sprint)referenceHydrator.Hydrate(
                            sprintEntity, typeof(Sprint), maxDepth, nextDepth
                        );
                    }

                    if (dto.ReleaseId != null)
                    {
                        int releaseId = (int)dto.ReleaseId;
                        Data.Entities.Release? releaseEntity =
                            _DBContext.Release.Find(releaseId) ??
                                throw new ModelNotFoundException(
                                    nameof(Release),
                                    releaseId.ToString()
                                );
                        model.Release = (Release)referenceHydrator.Hydrate(
                            releaseEntity, typeof(Release), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.BacklogItemPatchDto)
            {
                var dto = (API.Dtos.BacklogItemPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    Data.Entities.WorkflowState? workflowStateEntity =
                    _DBContext.WorkflowState.Find(dto.WorkflowStateId) ??
                        throw new ModelNotFoundException(
                            nameof(WorkflowState),
                            dto.WorkflowStateId.ToString()
                        );

                    model.WorkflowState = (WorkflowState)referenceHydrator.Hydrate(
                        workflowStateEntity, typeof(WorkflowState), maxDepth, nextDepth
                    );

                    if (dto.SprintId != null)
                    {
                        int sprintId = (int)dto.SprintId;
                        Data.Entities.Sprint? sprintEntity =
                            _DBContext.Sprint.Find(sprintId) ??
                                throw new ModelNotFoundException(
                                    nameof(Sprint),
                                    sprintId.ToString()
                                );
                        model.Sprint = (Sprint)referenceHydrator.Hydrate(
                            sprintEntity, typeof(Sprint), maxDepth, nextDepth
                        );
                    }

                    if (dto.ReleaseId != null)
                    {
                        int releaseId = (int)dto.ReleaseId;
                        Data.Entities.Release? releaseEntity =
                            _DBContext.Release.Find(releaseId) ??
                                throw new ModelNotFoundException(
                                    nameof(Release),
                                    releaseId.ToString()
                                );
                        model.Release = (Release)referenceHydrator.Hydrate(
                            releaseEntity, typeof(Release), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}
