using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
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

            var dbContext = (DBContext?)validationContext.GetService(typeof(DBContext)) ??
                throw new ServiceNotFoundException(nameof(DBContext));

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

                var backlogItem = dbContext.BacklogItem.Find(patchDto.ID) ??
                    throw new EntityNotFoundException(nameof(Project), patchDto.ID.ToString());

                projectId = backlogItem.Project.ID;
            }
            else
            {
                throw new Exception($"Attribute {GetType()} should only be " +
                    $"applied to {typeof(BacklogItemPostDto)} or {typeof(BacklogItemPatchDto)}");
            }

            // make sure the release belongs to the same project as the backlog item
            var release = dbContext.Release.Find(releaseId) ??
                throw new EntityNotFoundException(nameof(Release), releaseId.ToString());

            var project = dbContext.Project.Find(projectId) ??
                throw new EntityNotFoundException(nameof(Project), projectId.ToString());

            if (release.Project.ID == project.ID)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
