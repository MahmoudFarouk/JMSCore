using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMS.API.Models
{
    public partial class JMSDBContext : DbContext
    {
        public JMSDBContext()
        {
        }

        public JMSDBContext(DbContextOptions<JMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AssessmentQuestion> AssessmentQuestion { get; set; }
        public virtual DbSet<AssessmentResult> AssessmentResult { get; set; }
        public virtual DbSet<Checkpoint> Checkpoint { get; set; }
        public virtual DbSet<CodeException> CodeException { get; set; }
        public virtual DbSet<DriverStatusUpdate> DriverStatusUpdate { get; set; }
        public virtual DbSet<Journey> Journey { get; set; }
        public virtual DbSet<JourneyUpdate> JourneyUpdate { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserWorkForce> UserWorkForce { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=JMSDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(256);

                entity.Property(e => e.LicenseExpiryDate).HasColumnType("date");

                entity.Property(e => e.LicenseNo).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.UserGroupId)
                    .HasConstraintName("FK_AspNetUsers_UserGroup");

                entity.HasOne(d => d.UserWorkForce)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.UserWorkForceId)
                    .HasConstraintName("FK_AspNetUsers_UserWorkForce");
            });

            modelBuilder.Entity<AssessmentQuestion>(entity =>
            {
                entity.Property(e => e.Question).HasMaxLength(256);
            });

            modelBuilder.Entity<AssessmentResult>(entity =>
            {
                entity.Property(e => e.SubmittedBy).HasMaxLength(128);

                entity.HasOne(d => d.JourneyUpdate)
                    .WithMany(p => p.AssessmentResult)
                    .HasForeignKey(d => d.JourneyUpdateId)
                    .HasConstraintName("FK_AssessmentResult_JourneyUpdate");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssessmentResult)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_AssessmentResult_AssessmentQuestion");

                entity.HasOne(d => d.SubmittedByNavigation)
                    .WithMany(p => p.AssessmentResult)
                    .HasForeignKey(d => d.SubmittedBy)
                    .HasConstraintName("FK_AssessmentResult_AspNetUsers");
            });

            modelBuilder.Entity<Checkpoint>(entity =>
            {
                entity.Property(e => e.Lat).HasMaxLength(128);

                entity.Property(e => e.Lng).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<CodeException>(entity =>
            {
                entity.Property(e => e.AssemblyVersion).HasMaxLength(256);

                entity.Property(e => e.ClassName).HasMaxLength(256);

                entity.Property(e => e.ExceptionTime).HasColumnType("datetime");

                entity.Property(e => e.MachineName).HasMaxLength(256);

                entity.Property(e => e.MethodName).HasMaxLength(256);

                entity.Property(e => e.UserId).HasMaxLength(256);
            });

            modelBuilder.Entity<DriverStatusUpdate>(entity =>
            {
                entity.Property(e => e.DriverId).HasMaxLength(128);

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.DriverStatusUpdate)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_DriverStatusUpdate_AspNetUsers");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.DriverStatusUpdate)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK_DriverStatusUpdate_Journey");
            });

            modelBuilder.Entity<Journey>(entity =>
            {
                entity.Property(e => e.CargoType).HasMaxLength(128);

                entity.Property(e => e.CargoWeight).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DispatcherId).HasMaxLength(128);

                entity.Property(e => e.FromDestination).HasMaxLength(256);

                entity.Property(e => e.FromLat).HasMaxLength(128);

                entity.Property(e => e.FromLng).HasMaxLength(128);

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.Property(e => e.ToDistination).HasMaxLength(256);

                entity.Property(e => e.ToLat).HasMaxLength(128);

                entity.Property(e => e.ToLng).HasMaxLength(128);

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.DispatcherId)
                    .HasConstraintName("FK_Journey_AspNetUsers_Dispatcher");
            });

            modelBuilder.Entity<JourneyUpdate>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasMaxLength(128);

                entity.Property(e => e.Latitude).HasMaxLength(128);

                entity.Property(e => e.Longitude).HasMaxLength(128);

                entity.HasOne(d => d.Checkpoint)
                    .WithMany(p => p.JourneyUpdate)
                    .HasForeignKey(d => d.CheckpointId)
                    .HasConstraintName("FK_JourneyUpdate_Checkpoint");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.JourneyUpdate)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK_JourneyUpdate_Journey");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<UserWorkForce>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
