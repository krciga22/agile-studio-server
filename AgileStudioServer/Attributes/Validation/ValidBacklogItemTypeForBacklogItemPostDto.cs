using AgileStudioServer.Exceptions;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Attributes.Validation
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

            var _DBContext = (DBContext?)validationContext.GetService(typeof(DBContext)) ??
                throw new ServiceNotFoundException(nameof(DBContext));

            var dto = (BacklogItemPostDto)value;

            var project = _DBContext.Project.Find(dto.ProjectId) ??
                throw new EntityNotFoundException(nameof(Project), dto.ProjectId.ToString());

            var backlogItemType = _DBContext.BacklogItemType.Find(dto.BacklogItemTypeId) ??
                throw new EntityNotFoundException(nameof(BacklogItemType), dto.BacklogItemTypeId.ToString());

            _DBContext.Entry(project).Reference("BacklogItemTypeSchema").Load();
            _DBContext.Entry(backlogItemType).Reference("BacklogItemTypeSchema").Load();

            if (backlogItemType.BacklogItemTypeSchema.ID != project.BacklogItemTypeSchema.ID)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
