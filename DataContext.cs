using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jan24ft_bet_ca_kronosGR.Models;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace jan24ft_bet_ca_kronosGR
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Developer> Developers { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Developers)
                .WithOne(p => p.Role)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectType>()
                .HasMany(e => e.Projects)
                .WithOne(p => p.ProjectType)
                .HasForeignKey(e => e.ProjectTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
            .HasMany(e => e.Projects)
            .WithOne(p => p.Team)
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
            .HasMany(e => e.Developers)
            .WithOne(p => p.Team)
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}



