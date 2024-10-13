namespace AgileStudioServer.Application.Models
{
    public class ChildBacklogItemType
    {
        public int ID { get; set; }

        public BacklogItemType ChildType { get; set; } = null!;
        
        public BacklogItemType ParentType { get; set; } = null!;

        public BacklogItemTypeSchema Schema { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public User? CreatedBy { get; set; } = null!;

        public ChildBacklogItemType()
        {
            CreatedOn = DateTime.Now;
        }
    }
}