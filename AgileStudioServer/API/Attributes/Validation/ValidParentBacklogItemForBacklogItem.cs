using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidParentBacklogItemForBacklogItem : ValidationAttribute
    {
        public string GetErrorMessage() => "Invalid Parent Backlog Item for Backlog Item";

        public override bool RequiresValidationContext => true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            var backlogItemService = (BacklogItemService?)validationContext.GetService(typeof(BacklogItemService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemService));

            int? parentBacklogItemId = null;
            int? backlogItemProjectId = null;

            if (value is BacklogItemPostDto postDto)
            {
                if (postDto.ParentBacklogItemId != null)
                {
                    parentBacklogItemId = (int)postDto.ParentBacklogItemId;
                    backlogItemProjectId = postDto.ProjectId;
                }
            }
            else if (value is BacklogItemPatchDto patchDto)
            {
                if (patchDto.ParentBacklogItemId != null)
                {
                    parentBacklogItemId = (int)patchDto.ParentBacklogItemId;

                    var backlogItem = backlogItemService.Get(patchDto.ID) ??
                        throw new ModelNotFoundException(nameof(BacklogItem), patchDto.ID.ToString());

                    backlogItemProjectId = backlogItem.ProjectID;
                }
            }
            else
            {
                throw new Exception($"Attribute {GetType()} should only be " +
                    $"applied to {typeof(BacklogItemPostDto)} or {typeof(BacklogItemPatchDto)}");
            }

            // make sure the parent backlog item belongs to the same project as the backlog item
            if (parentBacklogItemId != null)
            {
                int id = (int)parentBacklogItemId;
                var parentBacklogItem = backlogItemService.Get(id) ??
                    throw new ModelNotFoundException(nameof(BacklogItem), id.ToString());

                if(parentBacklogItem.ProjectID != backlogItemProjectId)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }
    }
}
