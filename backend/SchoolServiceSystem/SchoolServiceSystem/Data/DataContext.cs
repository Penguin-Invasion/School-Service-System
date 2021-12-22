using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasMany<School>(u => u.Schools);
            /*
            modelBuilder.Entity<Bus>(entity =>
            {

            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasMany(e => e.Managers).WithOne();
            });

            modelBuilder.Entity<Service>(entity =>
            {

            });
            modelBuilder.Entity<Student>(entity =>
            {

            });
            modelBuilder.Entity<User>(entity =>
            {

            });*/
        }
    }
}
