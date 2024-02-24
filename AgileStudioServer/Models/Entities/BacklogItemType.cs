namespace AgileStudioServer.Models.Entities
{
    public class BacklogItemType
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemTypeSchema BacklogItemTypeSchema { get; set; } = null!;

        public Workflow Workflow { get; set; } = null!;

        public BacklogItemType(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}