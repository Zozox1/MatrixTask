using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IntimidatorsManagement_API.Models
{
   

        public class IntDBContext : DbContext
        {
            public IntDBContext(DbContextOptions options): base(options) 
            {
            }

       public IntDBContext()
        { }
            public DbSet<Intimidator> Intimidators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

      
            modelBuilder.Entity<Intimidator>().Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Intimidator>().HasKey(k => k.Id).HasName("Id");
            modelBuilder.Entity<Intimidator>().Property(p => p.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Intimidator>().Property(p => p.LastName).HasColumnName("LastName");
            modelBuilder.Entity<Intimidator>().Property(p => p.ScareDate).HasColumnName("ScareDate");
            modelBuilder.Entity<Intimidator>().Property(p => p.PhoneNumber).HasColumnName("FirsPhoneNumbertName");
            modelBuilder.Entity<Intimidator>().Property(p => p.TentaclesNumber).HasColumnName("TentaclesNumber");
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
