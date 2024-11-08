namespace AgileStudioServer.Application.Models
{
    public class Workflow
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedById { get; set; } = null!;

        public Workflow(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}