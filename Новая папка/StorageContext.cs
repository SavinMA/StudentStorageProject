using DataLayer.Models;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace DataLayer
{
    public class StorageContext : DbContext
    {
        private const string URL = @"hattie.db.elephantsql.com";
        private const string DatabaseName = @"clnvwnyg";
        private const string UserName = @"clnvwnyg";
        private const string Password = @"Z8fEIBYQgy5N4dISrZ-aweOhWyYz1puZ";

        private const string PostgresExtenstionForGuid = "uuid-ossp";
        private const string GenerateGuidSql = "uuid_generate_v4()";

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UniqueID> UniqueIds { get; set; }

        public StorageContext()
        {
            //Debug.WriteLine("try DataBase create");
            //if (Database.EnsureCreated())
            //{
            //    Debug.WriteLine("DataBase created");
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={URL};Database={DatabaseName};Username={UserName};Password={Password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension(PostgresExtenstionForGuid)
                .Entity<Group>()
                .Property(p => p.ID)
                .HasDefaultValueSql(GenerateGuidSql);

            modelBuilder.HasPostgresExtension(PostgresExtenstionForGuid)
                .Entity<Student>()
                .Property(p => p.ID)
                .HasDefaultValueSql(GenerateGuidSql);

            base.OnModelCreating(modelBuilder);
        }
    }
}
