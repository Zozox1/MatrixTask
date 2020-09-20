using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DoorsApi.Models
{
    public class DoorsDBContext: DbContext
    {
        public DoorsDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Door> Doors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Door>().Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<Door>().Property(p => p.Energy).HasColumnName("Energy");
            modelBuilder.Entity<Door>().Property(p => p.Available).HasColumnName("Available");
            modelBuilder.Entity<Door>().Property(p => p.LastUsed).HasColumnName("LastUsed");


            modelBuilder.Entity<Door>().HasData(new Door { Id=1,Energy=30,Available=true,LastUsed=null},new Door { Id = 2, Energy = 40, Available = true, LastUsed = null }, new Door { Id = 3, Energy = 20, Available = true, LastUsed = null }, new Door { Id = 4, Energy = 15, Available = true, LastUsed = null }, new Door { Id = 5, Energy = 25, Available = true, LastUsed = null }, new Door { Id = 6, Energy = 10, Available = true, LastUsed = null }, new Door { Id = 7, Energy = 20, Available = true, LastUsed = null }, new Door { Id = 8, Energy = 30, Available = true, LastUsed = null }, new Door { Id = 9, Energy = 10, Available = true, LastUsed = null }, new Door { Id = 10, Energy = 15, Available = true, LastUsed = null });

        }

    }
}
