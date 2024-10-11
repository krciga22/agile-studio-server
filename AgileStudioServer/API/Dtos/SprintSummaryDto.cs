
namespace AgileStudioServer.API.Dtos
{
    public class SprintSummaryDto
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public SprintSummaryDto(int id, int sprintNumber)
        {
            ID = id;
            SprintNumber = sprintNumber;
        }
    }
}
