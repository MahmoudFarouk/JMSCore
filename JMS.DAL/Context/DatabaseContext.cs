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
            optionsBuilder.UseSqlServer(@"server=jmssqlserver.database.windows.net;database=JMSDBNew;User ID=jmsdbadmin;password=P@ssw0rd;MultipleActiveResultSets=True;", b => b.MigrationsAssembly("JMS.API"));
            //optionsBuilder.UseSqlServer(@"Server=jmssqlserver.database.windows.net;database=JMSDB;User ID=jmsdbadmin;password=P@ssw0rd;", b => b.MigrationsAssembly("JMS.API"));

        }
    }
}
