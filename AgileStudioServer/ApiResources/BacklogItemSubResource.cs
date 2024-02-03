using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.ApiResources
{
    public class BacklogItemSubResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public BacklogItemSubResource(BacklogItem backlogItem)
        {
            ID = backlogItem.ID;
            Title = backlogItem.Title;
        }
    }
}
