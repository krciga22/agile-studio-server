
namespace AgileStudioServer.Application.Models
{
    public class Release
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int ProjectID { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Release(string title, int projectId)
        {
            Title = title;
            CreatedOn = DateTime.Now;
            ProjectID = projectId;
        }
    }
}