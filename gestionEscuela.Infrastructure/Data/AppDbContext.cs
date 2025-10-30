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
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasIndex(c => c.DocuNumber)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .HasIndex(c => c.StudentCode)
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.CourseCode)
            .IsUnique();
        
        // Relation 1:1 between Course and Schedule
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Schedule)
            .WithOne(s => s.Course)
            .HasForeignKey<Schedule>(s => s.CourseId);
        
        // Relation 1:1 between Enrollment and Grade
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Grade)
            .WithOne(g => g.Enrollment)
            .HasForeignKey<Grade>(g => g.EnrollmentId);
        
        base.OnModelCreating(modelBuilder);
    }

    // To create the tables on DB
    public DbSet<Student> students_tb { get; set; }
    public DbSet<Course> courses_tb { get; set; }
    public DbSet<Enrollment> enrollments_tb { get; set; }
    public DbSet<Teacher> teachers_tb { get; set; }
    public DbSet<Schedule> schedules_tb { get; set; }
    public DbSet<Grade> grades_tb { get; set; }
    
}