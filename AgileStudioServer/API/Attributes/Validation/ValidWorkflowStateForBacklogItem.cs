using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidWorkflowStateForBacklogItem : ValidationAttribute
    {
        public string GetErrorMessage() => "Invalid Workflow State for Backlog Item";

        public override bool RequiresValidationContext => true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            var backlogItemService = (BacklogItemService?)validationContext.GetService(typeof(BacklogItemService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemService));

            var backlogItemTypeService = (BacklogItemTypeService?)validationContext.GetService(typeof(BacklogItemTypeService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemTypeService));

            var workflowStateService = (WorkflowStateService?)validationContext.GetService(typeof(WorkflowStateService)) ??
                throw new ServiceNotFoundException(nameof(WorkflowStateService));

            int workflowStateId;
            int backlogItemTypeId;

            if (value is BacklogItemPostDto postDto)
            {
                workflowStateId = postDto.WorkflowStateId;
                backlogItemTypeId = postDto.BacklogItemTypeId;
            }
            else if (value is BacklogItemPatchDto patchDto)
            {
                workflowStateId = patchDto.WorkflowStateId;

                var backlogItem = backlogItemService.Get(patchDto.ID) ?? 
                    throw new ModelNotFoundException(nameof(BacklogItem), patchDto.ID.ToString());

                backlogItemTypeId = backlogItem.BacklogItemTypeID;
            }
            else
            {
                throw new Exception($"Attribute {GetType()} should only be " +
                    $"applied to {typeof(BacklogItemPostDto)} or {typeof(BacklogItemPatchDto)}");
            }

            // make sure the workflow state belongs to the same workflow associated with the backlog item's type

            var workflowState = workflowStateService.Get(workflowStateId) ??
                    throw new ModelNotFoundException(nameof(WorkflowState), workflowStateId.ToString());

            var backlogItemType = backlogItemTypeService.Get(backlogItemTypeId) ??
                    throw new ModelNotFoundException(nameof(BacklogItemType), backlogItemTypeId.ToString());

            if (workflowState.WorkflowId == backlogItemType.WorkflowID)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
