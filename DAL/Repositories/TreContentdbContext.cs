using System;
using System.Collections.Generic;
using DAL.Repositories.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DAL.Repositories.Entities;


namespace DAL.Repositories
{
    public partial class TreContentdbContext : DbContext
    {
        public TreContentdbContext()
        {
        }

        public TreContentdbContext(DbContextOptions<TreContentdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<AttributesName> AttributesNames { get; set; } = null!;
        public virtual DbSet<TreeNode> TreeNodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local); DataBase=TreContentdb;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.ApplicationKey);

                entity.ToTable("Application");

                entity.Property(e => e.ApplicationName).HasMaxLength(50);

                entity.Property(e => e.Owner).HasMaxLength(50);
            });

            modelBuilder.Entity<AttributesName>(entity =>
            {
                entity.ToTable("AttributesName");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AttributesName1)
                    .HasMaxLength(100)
                    .HasColumnName("AttributesName");

                entity.Property(e => e.TreeNodeId).HasColumnName("TreeNodeID");

                entity.HasOne(d => d.TreeNode)
                    .WithMany(p => p.AttributesNames)
                    .HasForeignKey(d => d.TreeNodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttributesName_TreeNode");
            });

            modelBuilder.Entity<TreeNode>(entity =>
            {
                entity.ToTable("TreeNode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateSave).HasMaxLength(50);

                entity.Property(e => e.Desc).HasMaxLength(50);

                entity.Property(e => e.NodeType).HasMaxLength(50);

                entity.Property(e => e.Owner).HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.ApplicationKeyNavigation)
                    .WithMany(p => p.TreeNodes)
                    .HasForeignKey(d => d.ApplicationKey)
                    .HasConstraintName("FK_TreeNode_Application1");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TreeNodes_TreeNodes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
