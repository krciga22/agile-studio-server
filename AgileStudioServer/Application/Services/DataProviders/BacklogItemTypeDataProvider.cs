﻿using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class BacklogItemTypeDataProvider
    {
        private readonly DBContext _DBContext;

        public BacklogItemTypeDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual BacklogItemTypeDto Create(BacklogItemTypePostDto dto)
        {
            var schema = _DBContext.BacklogItemTypeSchema.Find(dto.BacklogItemTypeSchemaId) ??
                throw new EntityNotFoundException(nameof(BacklogItemTypeSchema), dto.BacklogItemTypeSchemaId.ToString());

            var workflow = _DBContext.Workflow.Find(dto.WorkflowId) ??
                throw new EntityNotFoundException(nameof(Workflow), dto.WorkflowId.ToString());

            var backlogItemType = new BacklogItemType(dto.Title)
            {
                Description = dto.Description,
                BacklogItemTypeSchema = schema,
                Workflow = workflow,
            };

            _DBContext.Add(backlogItemType);
            _DBContext.SaveChanges();

            return new BacklogItemTypeDto(backlogItemType);
        }

        public virtual List<BacklogItemTypeDto> List()
        {
            List<BacklogItemTypeDto> apiResources = new();

            List<BacklogItemType> backlogItemTypes = _DBContext.BacklogItemType.ToList();
            backlogItemTypes.ForEach(backlogItemType =>
            {
                LoadReferences(backlogItemType);

                apiResources.Add(
                    new BacklogItemTypeDto(backlogItemType)
                );
            });

            return apiResources;
        }

        public virtual List<BacklogItemTypeSummaryDto> ListByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<BacklogItemTypeSummaryDto> apiResources = new();

            List<BacklogItemType> backlogItemTypes = _DBContext.BacklogItemType.Where(x => x.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId).ToList();
            backlogItemTypes.ForEach(backlogItemType =>
            {
                LoadReferences(backlogItemType);

                apiResources.Add(
                    new BacklogItemTypeSummaryDto(backlogItemType)
                );
            });

            return apiResources;
        }

        public virtual BacklogItemTypeDto? Get(int id)
        {
            BacklogItemType? backlogItemType = _DBContext.BacklogItemType.Find(id);
            if (backlogItemType is null)
            {
                return null;
            }

            LoadReferences(backlogItemType);

            return new BacklogItemTypeDto(backlogItemType);
        }

        public virtual BacklogItemTypeDto Update(int id, BacklogItemTypePatchDto dto)
        {
            var backlogItemType = _DBContext.BacklogItemType.Find(id) ??
                throw new EntityNotFoundException(nameof(BacklogItemType), id.ToString());

            backlogItemType.Title = dto.Title;
            backlogItemType.Description = dto.Description;
            _DBContext.SaveChanges();

            LoadReferences(backlogItemType);

            return new BacklogItemTypeDto(backlogItemType);
        }

        public virtual void Delete(int id)
        {
            var backlogItemType = _DBContext.BacklogItemType.Find(id) ??
                throw new EntityNotFoundException(nameof(BacklogItemType), id.ToString());

            _DBContext.BacklogItemType.Remove(backlogItemType);
            _DBContext.SaveChanges();
        }

        private void LoadReferences(BacklogItemType backlogItemType)
        {
            _DBContext.Entry(backlogItemType).Reference("CreatedBy").Load();
            _DBContext.Entry(backlogItemType).Reference("BacklogItemTypeSchema").Load();
            _DBContext.Entry(backlogItemType).Reference("Workflow").Load();
        }
    }
}
