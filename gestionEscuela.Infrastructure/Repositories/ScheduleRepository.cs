using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;
using gestionEscuela.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace gestionEscuela.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _context;

    public ScheduleRepository(AppDbContext context)
    {
        _context = context;
    }
    
    // --- Implementación de Métodos CRUD ---
    public async Task<Schedule?> GetByIdAsync(int id)
    {
        return await _context.schedules_tb.FindAsync(id);
    }

    public async Task<Schedule> CreateAsync(Schedule schedule)
    {
        _context.schedules_tb.Add(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<Schedule?> UpdateAsync(Schedule schedule)
    {
        _context.Entry(schedule).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ScheduleToDelete = await _context.schedules_tb.FindAsync(id);
        if (ScheduleToDelete == null) return false;
        
        _context.schedules_tb.Remove(ScheduleToDelete);
        await _context.SaveChangesAsync();
        return true;
    }

    // --- Implementación de Métodos Personalizados ---

    // Tarea: Listar horario por curso (1:1)
    public async Task<Schedule?> GetByCourseIdAsync(int courseId)
    {
        // Busca el horario que coincida con el CourseId
        return await _context.schedules_tb
            .FirstOrDefaultAsync(s => s.CourseId == courseId);
    }

    // Tarea: Validar que los horarios no se mezclen
    public async Task<IEnumerable<Schedule>> GetOverlappingSchedulesAsync(string day, TimeOnly startHour, TimeOnly endHour, string classRoom, int? scheduleIdToExclude = null)
    {
        var query = _context.schedules_tb
            .Where(s => s.Day == day &&
                        s.ClassRoom == classRoom && // Solo valida en la misma aula
                        s.StartHour < endHour &&   // Un horario existente termina después de que el nuevo empieza
                        s.EndHour > startHour);     // Un horario existente empieza antes de que el nuevo termine

        if (scheduleIdToExclude.HasValue)
        {
            // Al actualizar, excluimos el propio horario (ID) de la validación
            query = query.Where(s => s.Id != scheduleIdToExclude.Value);
        }

        return await query.ToListAsync();
    }
}