
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemHydrator : AbstractModelHydrator
    {
        public BacklogItemHydrator(
            DBContext dbContext,
            HydratorRegistry hydratorRegistry
        ) : base(dbContext, hydratorRegistry)
        {
            hydratorRegistry.Register(this);
        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.BacklogItem)
                || from == typeof(API.DtosNew.BacklogItemPostDto)
                || from == typeof(API.DtosNew.BacklogItemPatchDto)
            ) && to == typeof(BacklogItem);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth)
        {
            Object? model = null;

            if (to != typeof(BacklogItem))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Data.Entities.BacklogItem)
            {
                var entity = (Data.Entities.BacklogItem)from;
                model = new BacklogItem(entity.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.BacklogItemPostDto)
            {
                var dto = (API.DtosNew.BacklogItemPostDto)from;
                model = new BacklogItem(dto.Title);
                Hydrate(from, model, maxDepth, depth);
            }
            else if (from is API.DtosNew.BacklogItemPatchDto)
            {
                var dto = (API.DtosNew.BacklogItemPatchDto)from;
                var entity = _DBContext.BacklogItem.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItem), maxDepth, depth);
                    Hydrate(dto, model, maxDepth, depth);
                }
            }

            if (model == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth)
        {
            if (to is not BacklogItem)
            {
                throw new Exception("Unsupported to");
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

                if (nextDepth <= maxDepth)
                {
                    model.Project = (Project)_HydratorRegistry.Hydrate(
                        entity.Project, typeof(Project), maxDepth, nextDepth
                    );

                    model.BacklogItemType = (BacklogItemType)_HydratorRegistry.Hydrate(
                        entity.BacklogItemType, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    model.WorkflowState = (WorkflowState)_HydratorRegistry.Hydrate(
                        entity.WorkflowState, typeof(WorkflowState), maxDepth, nextDepth
                    );

                    if (entity.Sprint != null)
                    {
                        model.Sprint = (Sprint)_HydratorRegistry.Hydrate(
                            entity.Sprint, typeof(Sprint), maxDepth, nextDepth
                        );
                    }

                    if (entity.Release != null)
                    {
                        model.Release = (Release)_HydratorRegistry.Hydrate(
                            entity.Release, typeof(Release), maxDepth, nextDepth
                        );
                    }

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)_HydratorRegistry.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.DtosNew.BacklogItemPostDto)
            {
                var dto = (API.DtosNew.BacklogItemPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                Data.Entities.Project? projectEntity = 
                    _DBContext.Project.Find(dto.ProjectId) ??
                        throw new ModelNotFoundException(
                            nameof(Project),
                            dto.ProjectId.ToString()
                        );
                model.Project = (Project)_HydratorRegistry.Hydrate(
                    projectEntity, typeof(Project), maxDepth, nextDepth
                );

                Data.Entities.BacklogItemType? backlogItemTypeEntity =
                    _DBContext.BacklogItemType.Find(dto.BacklogItemTypeId) ??
                        throw new ModelNotFoundException(
                            nameof(BacklogItemType),
                            dto.BacklogItemTypeId.ToString()
                        );
                model.BacklogItemType = (BacklogItemType)_HydratorRegistry.Hydrate(
                    backlogItemTypeEntity, typeof(BacklogItemType), maxDepth, nextDepth
                );

                Data.Entities.WorkflowState? workflowStateEntity =
                    _DBContext.WorkflowState.Find(dto.WorkflowStateId) ??
                        throw new ModelNotFoundException(
                            nameof(WorkflowState),
                            dto.WorkflowStateId.ToString()
                        );
                model.WorkflowState = (WorkflowState)_HydratorRegistry.Hydrate(
                    workflowStateEntity, typeof(WorkflowState), maxDepth, nextDepth
                );

                if(dto.SprintId != null)
                {
                    int sprintId = (int) dto.SprintId;
                    Data.Entities.Sprint? sprintEntity =
                        _DBContext.Sprint.Find(sprintId) ??
                            throw new ModelNotFoundException(
                                nameof(Sprint),
                                sprintId.ToString()
                            );
                    model.Sprint = (Sprint)_HydratorRegistry.Hydrate(
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
                    model.Release = (Release)_HydratorRegistry.Hydrate(
                        releaseEntity, typeof(Release), maxDepth, nextDepth
                    );
                }
            }
            else if (from is API.DtosNew.BacklogItemPatchDto)
            {
                var dto = (API.DtosNew.BacklogItemPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                Data.Entities.WorkflowState? workflowStateEntity =
                    _DBContext.WorkflowState.Find(dto.WorkflowStateId) ??
                        throw new ModelNotFoundException(
                            nameof(WorkflowState),
                            dto.WorkflowStateId.ToString()
                        );
                model.WorkflowState = (WorkflowState)_HydratorRegistry.Hydrate(
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
                    model.Sprint = (Sprint)_HydratorRegistry.Hydrate(
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
                    model.Release = (Release)_HydratorRegistry.Hydrate(
                        releaseEntity, typeof(Release), maxDepth, nextDepth
                    );
                }
            }
        }
    }
}
