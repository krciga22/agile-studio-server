using AgileStudioServer.Models;

namespace AgileStudioServer.ApiResources
{
    public class BacklogItemApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public ProjectApiResource Project { get; set; }

        public BacklogItemTypeApiResource BacklogItemType { get; set; }

        public BacklogItemApiResource(BacklogItem backlogItem)
        {
            ID = backlogItem.ID;
            Title = backlogItem.Title;
            Description = backlogItem.Description;
            CreatedOn = backlogItem.CreatedOn;
            Project = new ProjectApiResource(backlogItem.Project);
            BacklogItemType = new BacklogItemTypeApiResource(backlogItem.BacklogItemType);
        }
    }
}
