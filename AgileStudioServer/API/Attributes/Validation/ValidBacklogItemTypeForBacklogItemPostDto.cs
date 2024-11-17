using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidBacklogItemTypeForBacklogItemPostDto : ValidationAttribute
    {
        public string GetErrorMessage() => "Invalid Backlog Item Type for Project";

        public override bool RequiresValidationContext => true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var projectService = (ProjectService?)validationContext.GetService(typeof(ProjectService)) ??
                throw new ServiceNotFoundException(nameof(ProjectService));

            var backlogItemTypeService = (BacklogItemTypeService?)validationContext.GetService(typeof(BacklogItemTypeService)) ??
                throw new ServiceNotFoundException(nameof(BacklogItemTypeService));

            var dto = (BacklogItemPostDto)value;

            var project = projectService.Get(dto.ProjectId) ??
                throw new ModelNotFoundException(nameof(Project), dto.ProjectId.ToString());

            var backlogItemType = backlogItemTypeService.Get(dto.BacklogItemTypeId) ??
                throw new ModelNotFoundException(nameof(BacklogItemType), dto.BacklogItemTypeId.ToString());

            if (backlogItemType.BacklogItemTypeSchemaID != project.BacklogItemTypeSchemaID)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
