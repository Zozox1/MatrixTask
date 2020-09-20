using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorkdayManagement_API.Models
{
    public class WorkDBContext:DbContext
    {

        public WorkDBContext(DbContextOptions options) : base(options)
        {
        }

        public WorkDBContext()
        { }

        public DbSet<Work> Work { get; set; }
        public DbSet<Goals> Goals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Work>().Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd(); 
            modelBuilder.Entity<Work>().Property(p => p.WorkerId).HasColumnName("WorkerId");
            modelBuilder.Entity<Work>().Property(p => p.WorkerEnergyUnits).HasColumnName("WorkerEnergyUnits");
            modelBuilder.Entity<Work>().Property(p => p.DeplatedDoors).HasColumnName("DeplatedDoors");
            modelBuilder.Entity<Work>().Property(p => p.DateOfWork).HasColumnName("DateOfWork");

            modelBuilder.Entity<Goals>().Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Goals>().Property(p => p.WorkerId).HasColumnName("WorkerId");
            modelBuilder.Entity<Goals>().Property(p => p.AccomplishDate).HasColumnName("AccomplishDate");
            modelBuilder.Entity<Goals>().Property(p => p.AccomplishGoals).HasColumnName("AccomplishGoals");
            modelBuilder.Entity<Goals>().Property(p => p.DailyDoors).HasColumnName("DailyDoors");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
