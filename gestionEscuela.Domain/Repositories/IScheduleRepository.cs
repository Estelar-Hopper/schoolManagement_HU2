using gestionEscuela.Domain.Entities;

namespace gestionEscuela.Domain.Repositories;

public interface IScheduleRepository
{
    Task<Schedule> GetByIdAsync(int Id);
    Task<Schedule> CreateAsync(Schedule schedule);
    Task<Schedule> UpdateAsync(Schedule schedule);
    Task<bool> DeleteAsync(int Id);

    //metodos especificos del schedule:

    //listar horario por curso
    Task<Schedule?> GetByCourseIdAsync(int courseId);

    //verificar que no se mexclen los horarios
    Task<IEnumerable<Schedule>> GetOverlappingSchedulesAsync(string day, TimeOnly startHour, TimeOnly endHour, string classRoom, int? scheduleIdToExclude = null);    
}