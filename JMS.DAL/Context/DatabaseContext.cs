using JMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserWorkForce> UserWorkForces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<AssessmentQuestion> AssessmentQuestion { get; set; }
        public virtual DbSet<AssessmentResult> AssessmentResult { get; set; }
        public virtual DbSet<Checkpoint> Checkpoint { get; set; }
        public virtual DbSet<CodeException> CodeException { get; set; }
        public virtual DbSet<Journey> Journey { get; set; }
        public virtual DbSet<JourneyUpdate> JourneyUpdate { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(@"Server=.;Initial Catalog=JMSDB;Persist Security Info=False;User ID=sa;Password=P@ssw0rd;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30", b => b.MigrationsAssembly("JMS.API"));
            
            optionsBuilder.UseSqlServer(@"Server=sql5041.site4now.net;Initial Catalog=DB_A37BE4_OBOXJMS;Persist Security Info=False;User ID=DB_A37BE4_OBOXJMS_admin;Password=161984@Mossad;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30", b => b.MigrationsAssembly("JMS.API"));

        }
    }
}
