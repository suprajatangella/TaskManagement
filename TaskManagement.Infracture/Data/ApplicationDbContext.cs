using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.InfraStructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TaskManagement.Domain.Entities.Task> Tasks { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(login => new { login.LoginProvider, login.ProviderKey }); // Composite key

            modelBuilder.Entity<TaskManagement.Domain.Entities.Task>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETDATE()"); // Default to current timestamp for SQL Server

            modelBuilder.Entity<TaskManagement.Domain.Entities.Task>()
                .Property(e => e.UpdatedDate)
                .HasDefaultValueSql("GETDATE()"); // Default to current timestamp for SQL Server
        }

    }
}
