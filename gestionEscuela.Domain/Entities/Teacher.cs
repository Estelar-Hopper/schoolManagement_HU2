namespace gestionEscuela.Domain.Entities;

public class Teacher : Person
{
    public int TeacherCode { get; set; }
    public string Career { get; set; }
    
    // relation 1:n
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}