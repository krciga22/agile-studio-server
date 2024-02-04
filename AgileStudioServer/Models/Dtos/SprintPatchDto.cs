using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Models.Dtos
{
    public class SprintPatchDto
    {
        [StringLength(255)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
    }
}
