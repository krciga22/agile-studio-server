using AgileStudioServer.Models;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Dtos
{
    public class BacklogItemTypeSchemaPatchDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemTypeSchemaPatchDto(string title)
        {
            Title = title;
        }
    }
}
