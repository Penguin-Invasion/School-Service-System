using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Utils;
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
        public DbSet<School> Schools { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        ID = 1,
                        Name = "Admin",
                        SurName = "Test",
                        Email = "admin",
                        Password = "123",
                        Role = Utils.Roles.Admin
                    },
                    new User
                    {
                        ID = 2,
                        Name = "Manager1",
                        SurName = "Test",
                        Email = "manager1",
                        Password = "123",
                        Role = Utils.Roles.Manager
                    },
                    new User
                    {
                        ID = 3,
                        Name = "Manager2",
                        SurName = "Test",
                        Email = "manager2",
                        Password = "123",
                        Role = Utils.Roles.Manager
                    },
                    new User
                    {
                        ID = 4,
                        Name = "Driver1",
                        SurName = "Test",
                        Email = "driver1",
                        Password = "123",
                        Role = Utils.Roles.Driver
                    },
                    new User
                    {
                        ID = 5,
                        Name = "Driver2",
                        SurName = "Test",
                        Email = "driver2",
                        Password = "123",
                        Role = Utils.Roles.Driver
                    }
                );
            modelBuilder.Entity<School>()
                .HasData(
                    new School
                    {
                        ID = 1,
                        Name = "Test Okul1",
                        SecretKey = Patcher.RandomString(30),
                        UserID = 2

                    },
                    new School
                    {
                        ID = 2,
                        Name = "Test Okul1",
                        SecretKey = Patcher.RandomString(30),
                        UserID = 3

                    }
                ); ;
            modelBuilder.Entity<Service>()
                .HasData(
                    new Service
                    {
                        ID = 1,
                        Name = "Test Servis1",
                        Plaque = "34 A 0001",
                        DriverID = 4,
                        SchoolID = 1
                    },
                    new Service
                    {
                        ID = 2,
                        Name = "Test Servis2",
                        Plaque = "34 A 0002",
                        DriverID = 5,
                        SchoolID = 2
                    }
                );
            /*
             * modelBuilder.Entity<Service>()
                .HasOne(s => s.User)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            */

            modelBuilder.Entity<User>()
                .HasOne(u => u.Service)
                .WithOne(s => s.Driver)
                .HasForeignKey<Service>(s => s.DriverID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasOne(u => u.School)
                .WithOne(s => s.User)
                .HasForeignKey<School>(s => s.UserID)
                .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<User>().HasMany<Service>(u => u.Service);

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
