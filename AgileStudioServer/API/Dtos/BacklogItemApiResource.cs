using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class BacklogItemApiResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserSubResource? CreatedBy { get; set; }

        public ProjectSubResource Project { get; set; }

        public BacklogItemTypeSubResource BacklogItemType { get; set; }

        public WorkflowStateSubResource WorkflowState { get; set; }

        public SprintSubResource? Sprint { get; set; } = null;

        public ReleaseSubResource? Release { get; set; } = null;

        public BacklogItemApiResource(BacklogItem backlogItem)
        {
            ID = backlogItem.ID;
            Title = backlogItem.Title;
            Description = backlogItem.Description;
            CreatedOn = backlogItem.CreatedOn;
            CreatedBy = backlogItem.CreatedBy is null ? null : new UserSubResource(backlogItem.CreatedBy);
            Project = new ProjectSubResource(backlogItem.Project);
            BacklogItemType = new BacklogItemTypeSubResource(backlogItem.BacklogItemType);
            WorkflowState = new WorkflowStateSubResource(backlogItem.WorkflowState);

            if (backlogItem.Sprint != null)
            {
                Sprint = new SprintSubResource(backlogItem.Sprint);
            }

            if (backlogItem.Release != null)
            {
                Release = new ReleaseSubResource(backlogItem.Release);
            }
        }
    }
}
