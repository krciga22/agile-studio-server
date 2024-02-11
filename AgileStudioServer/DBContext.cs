using AgileStudioServer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer
{
    public class DBContext : DbContext
    {
        public DbSet<Project> Project { get; set; }

        public DbSet<BacklogItem> BacklogItem { get; set; }

        public DbSet<BacklogItemType> BacklogItemType { get; set; }

        public DbSet<BacklogItemTypeSchema> BacklogItemTypeSchema { get; set; }

        public DbSet<Sprint> Sprint { get; set; }

        public DbSet<Release> Release { get; set; }

        public DBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(e => e.BacklogItemTypeSchema)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_project_backlog_item_type_schema_id");

            modelBuilder.Entity<BacklogItem>()
                .HasOne(e => e.BacklogItemType)
                .WithMany()
                .HasConstraintName("fk_backlog_item_backlog_item_type_id");

            modelBuilder.Entity<BacklogItemType>()
                .HasOne(e => e.BacklogItemTypeSchema)
                .WithMany()
                .HasConstraintName("fk_backlog_item_type_backlog_item_type_schema_id");
        }
    }
}
