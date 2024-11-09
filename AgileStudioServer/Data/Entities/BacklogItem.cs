namespace AgileStudioServer.Data.Entities
{
    public class BacklogItem
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public int ProjectID { get; set; }

        public Project Project { get; set; } = null!;

        public int? SprintID { get; set; } = null!;

        public Sprint? Sprint { get; set; } = null;

        public int? ReleaseID { get; set; } = null!;

        public Release? Release { get; set; } = null;

        public int BacklogItemTypeID { get; set; }

        public BacklogItemType BacklogItemType { get; set; } = null!;

        public int WorkflowStateID { get; set; }

        public WorkflowState WorkflowState { get; set; } = null!;

        public BacklogItem(string title, int projectID, int backlogItemTypeID, int workflowStateID)
        {
            Title = title;
            CreatedOn = DateTime.Now;
            ProjectID = projectID;
            BacklogItemTypeID = backlogItemTypeID;
            WorkflowStateID = workflowStateID;
        }
    }
}