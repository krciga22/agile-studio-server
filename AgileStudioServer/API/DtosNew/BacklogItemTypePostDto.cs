using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.DtosNew
{
    public class BacklogItemTypePostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int BacklogItemTypeSchemaId { get; set; }

        [Required]
        public int WorkflowId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemTypePostDto(string title, int backlogItemTypeSchemaId, int workflowId)
        {
            Title = title;
            BacklogItemTypeSchemaId = backlogItemTypeSchemaId;
            WorkflowId = workflowId;
        }
    }
}
