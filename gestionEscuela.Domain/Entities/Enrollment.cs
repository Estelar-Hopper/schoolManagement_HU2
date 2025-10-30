namespace gestionEscuela.Domain.Entities;

public class Enrollment
{
    // Properties:
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime InscriptionDate { get; set; }
    public string Status { get; set; }
    
    // Relation with other tables:
    public Student Student { get; set; }
    public Course Course { get; set; }
}