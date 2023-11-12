namespace AgileStudioServer.Models
{
    public class Project
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public Project(string title) { 
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}