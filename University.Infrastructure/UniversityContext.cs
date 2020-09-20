using Microsoft.EntityFrameworkCore;
using University.Domain.Entities;

namespace University.Infrastructure.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Gender).IsRequired();
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(40);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(40);
                entity.Property(p => p.MiddleName).IsRequired(false).HasMaxLength(60);
                entity.Property(p => p.UniqueName).IsRequired(false).HasMaxLength(16);
                entity.HasIndex(p => p.UniqueName).IsUnique();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<StudentGroup>().HasKey(k => new { k.GroupId, k.StudentId });

            modelBuilder
                .Entity<StudentGroup>()
                .HasOne(p => p.Group)
                .WithMany(p => p.StudentGroup)
                .HasForeignKey(p => p.GroupId);

            modelBuilder
                .Entity<StudentGroup>()
                .HasOne(p => p.Student)
                .WithMany(p => p.StudentGroup)
                .HasForeignKey(p => p.StudentId);
        }
    }
}
