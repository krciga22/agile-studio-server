namespace AgileStudioServer.Models
{
    public class BacklogItem
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemType BacklogItemType { get; set; } = null!;

        public BacklogItem(string title) { 
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}