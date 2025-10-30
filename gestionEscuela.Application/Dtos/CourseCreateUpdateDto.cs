namespace gestionEscuela.Application.Dtos;

public class CourseCreateUpdateDto
{
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public int TeacherId { get; set; }
    public int ClassRoom { get; set; }
    public bool CourseActive { get; set; }
}