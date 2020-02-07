using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FinalExam.Models;

namespace FinalExam.Data
{
    public partial class SportsContext : DbContext
    {
    //    public SportsContext()
    //    {
    //         public virtual DbSet<Players> Players { get; set; }
    //        public virtual DbSet<Sport> Sport { get; set; }
    //}
        public SportsContext(DbContextOptions<SportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Sport> Sport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost,1432;Initial Catalog=Sports;Persist Security Info=True;User ID=sa;Password=!2E45678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PK__Players__4A4E74A87EF1C2B1");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Contry).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.SportId).HasColumnName("SportID");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Players__SportID__38996AB5");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.Property(e => e.SportId).HasColumnName("SportID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.SportName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
