using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidReleaseForBacklogItem : ValidationAttribute
    {
        public string GetErrorMessage() => "Invalid Release for Backlog Item";

        public override bool RequiresValidationContext => true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            var backlogItemService = (BacklogItemService?)validationContext.GetService(typeof(BacklogItemService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemService));

            var releaseService = (ReleaseService?)validationContext.GetService(typeof(ReleaseService)) ??
                throw new ServiceNotFoundException(nameof(ReleaseService));

            var projectService = (ProjectService?)validationContext.GetService(typeof(ProjectService)) ??
                throw new ServiceNotFoundException(nameof(ProjectService));

            int releaseId;
            int projectId;

            if (value is BacklogItemPostDto postDto)
            {
                if (postDto.ReleaseId is null)
                {
                    return ValidationResult.Success;
                }

                releaseId = (int)postDto.ReleaseId;
                projectId = postDto.ProjectId;
            }
            else if (value is BacklogItemPatchDto patchDto)
            {
                if (patchDto.ReleaseId is null)
                {
                    return ValidationResult.Success;
                }

                releaseId = (int)patchDto.ReleaseId;

                var backlogItem = backlogItemService.Get(patchDto.ID) ??
                    throw new ModelNotFoundException(nameof(BacklogItem), patchDto.ID.ToString());

                projectId = backlogItem.ProjectID;
            }
            else
            {
                throw new Exception($"Attribute {GetType()} should only be " +
                    $"applied to {typeof(BacklogItemPostDto)} or {typeof(BacklogItemPatchDto)}");
            }

            // make sure the release belongs to the same project as the backlog item
            var release = releaseService.Get(releaseId) ??
                throw new ModelNotFoundException(nameof(Release), releaseId.ToString());

            var project = projectService.Get(projectId) ??
                throw new ModelNotFoundException(nameof(Project), projectId.ToString());

            if (release.ProjectID == project.ID)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
