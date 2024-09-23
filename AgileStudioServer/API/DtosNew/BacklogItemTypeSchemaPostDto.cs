using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.DtosNew
{
    public class BacklogItemTypeSchemaPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemTypeSchemaPostDto(string title)
        {
            Title = title;
        }
    }
}
