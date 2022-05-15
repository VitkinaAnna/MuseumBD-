using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MuseunBD
{
    public partial class MuseumContext : DbContext
    {
        public MuseumContext()
        {
        }

        public MuseumContext(DbContextOptions<MuseumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dinosaur> Dinosaurs { get; set; } = null!;
        public virtual DbSet<Exhibition> Exhibitions { get; set; } = null!;
        public virtual DbSet<Hall> Halls { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-EFQ1BJ6\\SQLEXPRESS; Database=Museum; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dinosaur>(entity =>
            {
                entity.Property(e => e.DinosaurId).HasColumnName("dinosaurId");

                entity.Property(e => e.HallId).HasColumnName("hallId");

                entity.Property(e => e.Lifetime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lifetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Dinosaurs)
                    .HasForeignKey(d => d.HallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dinosaurs_Halls");
            });

            modelBuilder.Entity<Exhibition>(entity =>
            {
                entity.Property(e => e.ExhibitionId).HasColumnName("exhibitionId");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("price");

                entity.Property(e => e.WorkerId).HasColumnName("workerId");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Exhibitions)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exhibitions_Workers");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.Property(e => e.HallId).HasColumnName("hallId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.WorkerId).HasColumnName("workerId");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Halls)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Halls_Workers");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.PositionId).HasColumnName("positionId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("ticketId");

                entity.Property(e => e.ExhibitionId).HasColumnName("exhibitionId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Exhibition)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ExhibitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Exhibitions");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.WorkerId).HasColumnName("workerId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PositionId).HasColumnName("positionId");

                entity.Property(e => e.Salary)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("salary");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workers_Positions");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
