using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
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

            var dbContext = (DBContext?)validationContext.GetService(typeof(DBContext)) ??
                throw new ServiceNotFoundException(nameof(DBContext));

            var dto = (BacklogItemPostDto)value;

            var project = dbContext.Project.Find(dto.ProjectId) ??
                throw new EntityNotFoundException(nameof(Project), dto.ProjectId.ToString());

            var backlogItemType = dbContext.BacklogItemType.Find(dto.BacklogItemTypeId) ??
                throw new EntityNotFoundException(nameof(BacklogItemType), dto.BacklogItemTypeId.ToString());

            dbContext.Entry(project).Reference("BacklogItemTypeSchema").Load();
            dbContext.Entry(backlogItemType).Reference("BacklogItemTypeSchema").Load();

            if (backlogItemType.BacklogItemTypeSchema.ID != project.BacklogItemTypeSchema.ID)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
