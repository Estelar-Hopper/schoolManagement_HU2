namespace gestionEscuela.Application.Dtos;

public class StudentCreateUpdateDto
{
    public string Name { get; set; }
    public string DocNumber { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int StudentCode { get; set; }
    public int Grade { get; set; }
    
}