﻿// <auto-generated />
using System;
using AgileStudioServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgileStudioServer.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AgileStudioServer.Models.BacklogItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("BacklogItemTypeID")
                        .HasColumnType("int")
                        .HasColumnName("backlog_item_type_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_backlog_item");

                    b.HasIndex("BacklogItemTypeID")
                        .HasDatabaseName("ix_backlog_item_backlog_item_type_id");

                    b.ToTable("backlog_item", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.BacklogItemType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("BacklogItemTypeSchemaID")
                        .HasColumnType("int")
                        .HasColumnName("backlog_item_type_schema_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_backlog_item_type");

                    b.HasIndex("BacklogItemTypeSchemaID")
                        .HasDatabaseName("ix_backlog_item_type_backlog_item_type_schema_id");

                    b.ToTable("backlog_item_type", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.BacklogItemTypeSchema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_backlog_item_type_schemas");

                    b.HasIndex("ProjectID")
                        .HasDatabaseName("ix_backlog_item_type_schemas_project_id");

                    b.ToTable("backlog_item_type_schemas", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_projects");

                    b.ToTable("projects", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.BacklogItem", b =>
                {
                    b.HasOne("AgileStudioServer.Models.BacklogItemType", "BacklogItemType")
                        .WithMany()
                        .HasForeignKey("BacklogItemTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_backlog_item_backlog_item_type_id");

                    b.Navigation("BacklogItemType");
                });

            modelBuilder.Entity("AgileStudioServer.Models.BacklogItemType", b =>
                {
                    b.HasOne("AgileStudioServer.Models.BacklogItemTypeSchema", "BacklogItemTypeSchema")
                        .WithMany()
                        .HasForeignKey("BacklogItemTypeSchemaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_backlog_item_type_backlog_item_type_schema_id");

                    b.Navigation("BacklogItemTypeSchema");
                });

            modelBuilder.Entity("AgileStudioServer.Models.BacklogItemTypeSchema", b =>
                {
                    b.HasOne("AgileStudioServer.Models.Project", "Project")
                        .WithMany("BacklogItemTypeSchemas")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_backlog_item_type_schemas_projects_project_id");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("AgileStudioServer.Models.Project", b =>
                {
                    b.Navigation("BacklogItemTypeSchemas");
                });
#pragma warning restore 612, 618
        }
    }
}
