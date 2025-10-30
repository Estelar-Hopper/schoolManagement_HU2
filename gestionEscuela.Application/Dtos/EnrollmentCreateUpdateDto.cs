namespace gestionEscuela.Application.Dtos;

public class EnrollmentCreateUpdateDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime InscriptionDate { get; set; }
    public string Status { get; set; }
}