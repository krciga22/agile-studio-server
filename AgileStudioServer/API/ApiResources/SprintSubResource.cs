﻿using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.ApiResources
{
    public class SprintSubResource
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public SprintSubResource(Sprint sprint)
        {
            ID = sprint.ID;
            SprintNumber = sprint.SprintNumber;
        }
    }
}