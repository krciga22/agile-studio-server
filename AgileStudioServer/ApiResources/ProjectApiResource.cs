using AgileStudioServer.Models;

namespace AgileStudioServer.ApiResources
{
    public class ProjectApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemTypeSchema BacklogItemTypeSchema { get; set; }

        public ProjectApiResource(Project project)
        {
            ID = project.ID;
            Title = project.Title;
            Description = project.Description;
            CreatedOn = project.CreatedOn;
            BacklogItemTypeSchema = project.BacklogItemTypeSchema;
        }
    }
}
