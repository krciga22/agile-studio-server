using AgileStudioServer.Exceptions;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Attributes.Validation
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

            var dbContext = (DBContext?)validationContext.GetService(typeof(DBContext)) ??
                throw new ServiceNotFoundException(nameof(DBContext));
            
            int sprintId;
            int projectId;

            if (value is BacklogItemPostDto)
            {
                var postDto = (BacklogItemPostDto)value;
                if (postDto.SprintId is null)
                {
                    return ValidationResult.Success;
                }

                sprintId = (int)postDto.SprintId;
                projectId = postDto.ProjectId;
            }
            else if (value is BacklogItemPatchDto)
            {
                var patchDto = (BacklogItemPatchDto)value;
                if (patchDto.SprintId is null)
                {
                    return ValidationResult.Success;
                }

                sprintId = (int)patchDto.SprintId;

                var backlogItem = dbContext.BacklogItem.Find(patchDto.ID) ??
                    throw new EntityNotFoundException(nameof(Project), patchDto.ID.ToString());

                projectId = backlogItem.Project.ID;
            }
            else
            {
                throw new Exception($"Attribute {this.GetType()} should only be " +
                    $"applied to {typeof(BacklogItemPostDto)} or {typeof(BacklogItemPatchDto)}");
            }

            // make sure the sprint belongs to the same project as the backlog item
            var sprint = dbContext.Sprints.Find(sprintId) ?? 
                throw new EntityNotFoundException(nameof(Sprint), sprintId.ToString());

            var project = dbContext.Project.Find(projectId) ??
                throw new EntityNotFoundException(nameof(Project), projectId.ToString());

            if(sprint.Project.ID == project.ID)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
