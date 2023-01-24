using ConsoleApp3.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class AutoPartsTaskContext : DbContext
    {
        public AutoPartsTaskContext() : base()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Complectation> Complectations { get; set; }
        public DbSet<PrimaryPart> PrimaryParts { get; set; }
        public DbSet<SubPart> SubParts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AutoPartsTask;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Brand>()
            //    .HasOne<Grade>(s => s.Grade)
            //    .WithMany(g => g.Students)
            //    .HasForeignKey(s => s.CurrentGradeId);
        }
    }
}
