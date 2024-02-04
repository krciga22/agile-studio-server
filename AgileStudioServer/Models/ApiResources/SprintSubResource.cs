using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
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
