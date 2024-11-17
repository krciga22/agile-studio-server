using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidSprintForBacklogItem : ValidationAttribute
    {
        public string GetErrorMessage() => "Invalid Sprint for Backlog Item";

        public override bool RequiresValidationContext => true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            var backlogItemService = (BacklogItemService?)validationContext.GetService(typeof(BacklogItemService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemService));

            var sprintService = (SprintService?)validationContext.GetService(typeof(SprintService)) ??
                throw new ServiceNotFoundException(nameof(SprintService));

            var projectService = (ProjectService?)validationContext.GetService(typeof(ProjectService)) ??
                throw new ServiceNotFoundException(nameof(ProjectService));

            int sprintId;
            int projectId;

            if (value is BacklogItemPostDto postDto)
            {
                if (postDto.SprintId is null)
                {
                    return ValidationResult.Success;
                }

                sprintId = (int)postDto.SprintId;
                projectId = postDto.ProjectId;
            }
            else if (value is BacklogItemPatchDto patchDto)
            {
                if (patchDto.SprintId is null)
                {
                    return ValidationResult.Success;
                }

                sprintId = (int)patchDto.SprintId;

                var backlogItem = backlogItemService.Get(patchDto.ID) ??
                    throw new ModelNotFoundException(nameof(BacklogItem), patchDto.ID.ToString());

                projectId = backlogItem.ProjectID;
            }
            else
            {
                throw new Exception($"Attribute {GetType()} should only be " +
                    $"applied to {typeof(BacklogItemPostDto)} or {typeof(BacklogItemPatchDto)}");
            }

            // make sure the sprint belongs to the same project as the backlog item
            var sprint = sprintService.Get(sprintId) ??
                throw new ModelNotFoundException(nameof(Sprint), sprintId.ToString());

            var project = projectService.Get(projectId) ??
                throw new ModelNotFoundException(nameof(Project), projectId.ToString());

            if (sprint.ProjectID == project.ID)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
