using AgileStudioServer.Exceptions;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Attributes.Validation
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

            var dbContext = (DBContext?)validationContext.GetService(typeof(DBContext)) ??
                throw new ServiceNotFoundException(nameof(DBContext));
            
            int workflowStateId;
            int backlogItemTypeId;

            if (value is BacklogItemPostDto postDto)
            {
                workflowStateId = (int)postDto.WorkflowStateId;
                backlogItemTypeId = postDto.BacklogItemTypeId;
            }
            else if (value is BacklogItemPatchDto patchDto)
            {
                workflowStateId = (int)patchDto.WorkflowStateId;

                var backlogItem = dbContext.BacklogItem.Find(patchDto.ID) ??
                    throw new EntityNotFoundException(nameof(BacklogItem), patchDto.ID.ToString());

                backlogItemTypeId = backlogItem.BacklogItemType.ID;
            }
            else
            {
                throw new Exception($"Attribute {this.GetType()} should only be " +
                    $"applied to {typeof(BacklogItemPostDto)} or {typeof(BacklogItemPatchDto)}");
            }

            // make sure the workflow state belongs to the same workflow associated with the backlog item's type

            var workflowState = dbContext.WorkflowState.Find(workflowStateId) ?? 
                throw new EntityNotFoundException(nameof(WorkflowState), workflowStateId.ToString());

            var backlogItemType = dbContext.BacklogItemType.Find(backlogItemTypeId) ??
                throw new EntityNotFoundException(nameof(BacklogItemType), backlogItemTypeId.ToString());

            if(workflowState.Workflow.ID == backlogItemType.Workflow.ID)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
