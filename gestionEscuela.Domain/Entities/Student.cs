namespace gestionEscuela.Domain.Entities;

public class Student : Person
{
    // Properties
    public int Id { get; set; }
    public int StudentCode { get; set; }
    public int Grade { get; set; }
    
    
    // Relation 1:n
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}