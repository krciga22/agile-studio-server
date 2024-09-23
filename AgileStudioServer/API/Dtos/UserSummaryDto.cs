using AgileStudioServer.Data.Entities;

namespace AgileStudioServer.API.Dtos
{
    public class UserSummaryDto
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserSummaryDto(User user)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
