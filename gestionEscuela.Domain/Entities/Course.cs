namespace gestionEscuela.Domain.Entities;

public class Course
{
    // properties
    public int Id { get; set; }
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public int TeacherId { get; set; }
    public int ClassRoom { get; set; }
    public bool CourseActive { get; set; }
    
    // Relation 1:n
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}