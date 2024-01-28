using AgileStudioServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer
{
    public class DBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<BacklogItem> BacklogItem { get; set; }

        public DbSet<BacklogItemType> BacklogItemType { get; set; }

        public DbSet<BacklogItemTypeSchema> BacklogItemTypeSchemas { get; set; }

        public DBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
    }
}
