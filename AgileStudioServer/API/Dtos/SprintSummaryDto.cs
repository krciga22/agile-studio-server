using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class SprintSummaryDto
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public SprintSummaryDto(Sprint sprint)
        {
            ID = sprint.ID;
            SprintNumber = sprint.SprintNumber;
        }
    }
}
