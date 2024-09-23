using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.DtosNew
{
    public class ReleasePatchDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public ReleasePatchDto(string title)
        {
            Title = title;
        }
    }
}
