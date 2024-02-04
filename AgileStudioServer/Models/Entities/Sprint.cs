namespace AgileStudioServer.Models.Entities
{
    public class Sprint
    {
        public int ID { get; set; }

        public int SprintNumber { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Sprint(int sprintNumber)
        {
            SprintNumber = sprintNumber;
            CreatedOn = DateTime.Now;
        }
    }
}