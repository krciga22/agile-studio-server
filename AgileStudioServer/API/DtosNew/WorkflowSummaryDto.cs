
namespace AgileStudioServer.API.DtosNew
{
    public class WorkflowSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSummaryDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
