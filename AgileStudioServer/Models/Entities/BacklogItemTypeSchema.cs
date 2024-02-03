﻿using AgileStudioServer.Models.Dtos;

namespace AgileStudioServer.Models.Entities
{
    public class BacklogItemTypeSchema
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public BacklogItemTypeSchema(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}