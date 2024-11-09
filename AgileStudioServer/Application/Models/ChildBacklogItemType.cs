namespace AgileStudioServer.Application.Models
{
    public class ChildBacklogItemType
    {
        public int ID { get; set; }

        public int ChildTypeID { get; set; }
        
        public int ParentTypeID { get; set; }

        public int SchemaID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public ChildBacklogItemType(int childTypeId, int parentTypeId, int schemaId)
        {
            CreatedOn = DateTime.Now;
            ChildTypeID = childTypeId;
            ParentTypeID = parentTypeId;
            SchemaID = schemaId;
        }
    }
}