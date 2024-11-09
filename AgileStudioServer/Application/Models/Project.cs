using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Application.Models
{
    public class Project
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public int BacklogItemTypeSchemaID { get; set; }

        public Project(string title, int backlogItemTypeSchemaID)
        {
            Title = title;
            CreatedOn = DateTime.Now;
            BacklogItemTypeSchemaID = backlogItemTypeSchemaID;
        }
    }
}