namespace AgileStudioServer.Models.Entities
{
    public class BacklogItem
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public User? CreatedBy { get; set; } = null!;

        public Project Project { get; set; } = null!;

        public Sprint? Sprint { get; set; } = null;

        public Release? Release { get; set; } = null;

        public BacklogItemType BacklogItemType { get; set; } = null!;

        public WorkflowState WorkflowState { get; set; } = null!;

        public BacklogItem(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}