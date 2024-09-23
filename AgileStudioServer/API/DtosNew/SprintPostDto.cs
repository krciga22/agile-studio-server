using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.DtosNew
{
    public class SprintPostDto
    {
        [Required]
        public int ProjectId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public SprintPostDto(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
