
namespace AgileStudioServer.Data.Entities
{
    public class ChildBacklogItemType
    {
        public int ID { get; set; }

        public int ChildTypeID { get; set; }

        public BacklogItemType ChildType { get; set; } = null!;

        public int ParentTypeID { get; set; }

        public BacklogItemType ParentType { get; set; } = null!;

        public int SchemaID { get; set; }

        public BacklogItemTypeSchema Schema { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public ChildBacklogItemType(int childTypeID, int parentTypeID, int schemaID)
        {
            CreatedOn = DateTime.Now;
            ChildTypeID = childTypeID;
            ParentTypeID = parentTypeID;
            SchemaID = schemaID;
        }
    }
}