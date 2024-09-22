﻿using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.ApiResources
{
    public class WorkflowSubResource
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public WorkflowSubResource(Workflow workflow)
        {
            ID = workflow.ID;
            Title = workflow.Title;
        }
    }
}