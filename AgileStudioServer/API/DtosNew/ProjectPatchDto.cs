﻿using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.API.DtosNew
{
    public class ProjectPatchDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public ProjectPatchDto(string title)
        {
            Title = title;
        }
    }
}
