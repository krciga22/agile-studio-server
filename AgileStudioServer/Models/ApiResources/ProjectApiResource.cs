using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class ProjectApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSubResource? CreatedBy { get; set; }

        public BacklogItemTypeSchemaSubResource BacklogItemTypeSchema { get; set; }

        public ProjectApiResource(Project project)
        {
            ID = project.ID;
            Title = project.Title;
            Description = project.Description;
            CreatedOn = project.CreatedOn;
            CreatedBy = project.CreatedBy is null ? null : new UserSubResource(project.CreatedBy);
            BacklogItemTypeSchema = new BacklogItemTypeSchemaSubResource(project.BacklogItemTypeSchema);
        }
    }
}
