using DomainLayer.Models;

using Microsoft.EntityFrameworkCore;

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
        //public DbSet<StudentGroup> StudentsGroups { get; set; }

        public StorageContext()
        {
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

            modelBuilder.HasPostgresExtension(PostgresExtenstionForGuid)
                .Entity<UniqueID>()
                .Property(p => p.ID)
                .HasDefaultValueSql(GenerateGuidSql);

            modelBuilder.Entity<UniqueID>()
                .HasIndex(p => p.UID)
                .IsUnique(true);

            modelBuilder.Entity<StudentGroup>()
               .HasKey(sg => new { sg.GroupGuid, sg.StudentGuid });

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Student)
                .WithMany(s => s.Groups)
                .HasForeignKey(sg => sg.StudentGuid);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Group)
                .WithMany(s => s.Students)
                .HasForeignKey(sg => sg.GroupGuid);

            base.OnModelCreating(modelBuilder);
        }
    }
}
