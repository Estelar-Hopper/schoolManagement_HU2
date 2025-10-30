namespace gestionEscuela.Domain.Entities;

public class Grade
{
    public int Id { get; set; }
    public int EnrollmentId { get; set; }
    public double Score { get; set; }
    
    // Relation 
    public Enrollment Enrollment { get; set; }
}