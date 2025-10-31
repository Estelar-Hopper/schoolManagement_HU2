namespace gestionEscuela.Application.Dtos;

// Objeto para transferir datos para crear o actualizar un horario
public class ScheduleCreateUpdateDto
{
    public int CourseId { get; set; }
    public string Day { get; set; }
    public TimeOnly StartHour { get; set; }
    public TimeOnly EndHour { get; set; }
    public string ClassRoom { get; set; }
    public int StudentsAmount { get; set; } // Tu requisito de "cupo máximo"
}