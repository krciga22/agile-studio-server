using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class BacklogItemApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public ProjectSubResource Project { get; set; }

        public BacklogItemTypeSubResource BacklogItemType { get; set; }

        public SprintSubResource? Sprint { get; set; } = null;

        public BacklogItemApiResource(BacklogItem backlogItem)
        {
            ID = backlogItem.ID;
            Title = backlogItem.Title;
            Description = backlogItem.Description;
            CreatedOn = backlogItem.CreatedOn;
            Project = new ProjectSubResource(backlogItem.Project);
            BacklogItemType = new BacklogItemTypeSubResource(backlogItem.BacklogItemType);

            if (backlogItem.Sprint != null) {
                Sprint = new SprintSubResource(backlogItem.Sprint);
            }
        }
    }
}
