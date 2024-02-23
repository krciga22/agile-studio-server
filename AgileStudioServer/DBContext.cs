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

        public DbSet<User> User { get; set; }

        public DbSet<Workflow> Workflow { get; set; }

        public DbSet<WorkflowState> WorkflowState { get; set; }

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

            modelBuilder.Entity<BacklogItem>()
                .HasOne(e => e.WorkflowState)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_backlog_item_workflow_state_id");

            modelBuilder.Entity<BacklogItemType>()
                .HasOne(e => e.BacklogItemTypeSchema)
                .WithMany()
                .HasConstraintName("fk_backlog_item_type_backlog_item_type_schema_id");

            modelBuilder.Entity<BacklogItemType>()
                .HasOne(e => e.Workflow)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_backlog_item_type_workflow_workflow_id");
        }
    }
}
