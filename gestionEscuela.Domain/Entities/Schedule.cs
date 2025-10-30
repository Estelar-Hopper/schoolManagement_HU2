namespace gestionEscuela.Domain.Entities;

public class Schedule
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Day { get; set; }
    public TimeOnly StartHour { get; set; }
    public TimeOnly EndHour { get; set; }
    public string ClassRoom { get; set; }
    public int StudentsAmount { get; set; }
    
    // Relation with other tables:
    public Course Course { get; set; }
}