using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.Linq;

namespace InfytainmentDAL.Models
{
    public partial class InfytainmentDBContext : DbContext
    {
        public InfytainmentDBContext()
        {
        }

        public InfytainmentDBContext(DbContextOptions<InfytainmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Screens> Screens { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }
        public virtual DbSet<ShowTimings> ShowTimings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("InfytainmentDBConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__MovieID__2D27B809");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.ScreenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__ScreenI__2E1BDC42");

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__ShowId__2F10007B");
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.HasKey(e => e.MovieId);

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Screens>(entity =>
            {
                entity.HasKey(e => e.ScreenId);
            });

            modelBuilder.Entity<Seats>(entity =>
            {
                entity.HasKey(e => e.SeatId);

                entity.Property(e => e.SeatId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Seats__BookId__300424B4");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.ScreenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seats__ScreenId__30F848ED");
            });

            modelBuilder.Entity<ShowTimings>(entity =>
            {
                entity.HasKey(e => e.ShowId);

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ShowTimings)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__ShowTimin__Movie__31EC6D26");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ShowTimings)
                    .HasForeignKey(d => d.ScreenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShowTimin__Scree__32E0915F");
            });
        }
    }
}
