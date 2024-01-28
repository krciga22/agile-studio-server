namespace AgileStudioServer.Models
{
    public class BacklogItemType
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemTypeSchema BacklogItemTypeSchema { get; set; } = null!;

        public BacklogItemType(string title) { 
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}