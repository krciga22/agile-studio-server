using AgileStudioServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
    }
}
