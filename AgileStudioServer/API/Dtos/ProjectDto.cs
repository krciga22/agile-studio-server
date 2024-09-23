using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class ProjectDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSummaryDto? CreatedBy { get; set; }

        public BacklogItemTypeSchemaSummaryDto BacklogItemTypeSchema { get; set; }

        public ProjectDto(Project project)
        {
            ID = project.ID;
            Title = project.Title;
            Description = project.Description;
            CreatedOn = project.CreatedOn;
            CreatedBy = project.CreatedBy is null ? null : new UserSummaryDto(project.CreatedBy);
            BacklogItemTypeSchema = new BacklogItemTypeSchemaSummaryDto(project.BacklogItemTypeSchema);
        }
    }
}
