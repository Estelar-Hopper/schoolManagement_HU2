using System.Data.Common;
using gestionEscuela.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gestionEscuela.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options )
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.DocuNumber)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.StudentCode)
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.CourseCode)
            .IsUnique();
        
        // we can add restrictions to the teachers table here.
        
        base.OnModelCreating(modelBuilder);
    }

    // To create the tables on DB
    public DbSet<Student> students_tb { get; set; }
    public DbSet<Course> courses_tb { get; set; }
    public DbSet<Enrollment> enrollments_tb { get; set; }
    
    
}