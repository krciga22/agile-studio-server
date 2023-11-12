using AgileStudioServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer
{
    public class DBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
    }
}
