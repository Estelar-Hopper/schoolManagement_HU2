namespace gestionEscuela.Application.Dtos;

public class TeacherCreateUpdateDto
{
    public string Name { get; set; }
    public string DocuNumber { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int TeacherCode { get; set; }
    public string Career { get; set; }
}