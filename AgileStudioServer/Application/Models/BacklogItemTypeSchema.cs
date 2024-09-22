namespace AgileStudioServer.Application.Models
{
    public class BacklogItemTypeSchema
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemTypeSchema(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}