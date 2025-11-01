using gestionEscuela.Application.Dtos;
using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;

namespace gestionEscuela.Application.Services;

public class ScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    // Usamos IGenericRepository<Course> porque ya está inyectado en Program.cs
    private readonly IGenericRepository<Course> _courseRepository; 

    public ScheduleService(IScheduleRepository scheduleRepository, IGenericRepository<Course> courseRepository)
    {
        _scheduleRepository = scheduleRepository;
        _courseRepository = courseRepository;
    }

    // Tarea: Crear un horario
    public async Task<Schedule> CreateAsync(ScheduleCreateUpdateDto dto)
    {
        // 1. Validar que el curso (Course) al que se asigna existe
        var course = await _courseRepository.GetByIdAsync(dto.CourseId);
        if (course == null)
            throw new KeyNotFoundException($"El curso con ID {dto.CourseId} no existe.");

        // 2. Validar Lógica 1:1 (Tu requisito)
        var existingScheduleForCourse = await _scheduleRepository.GetByCourseIdAsync(dto.CourseId);
        if (existingScheduleForCourse != null)
        {
            throw new InvalidOperationException($"Conflicto: El curso '{course.CourseName}' ya tiene un horario asignado (ID: {existingScheduleForCourse.Id}).");
        }

        // 3. Tarea: Validar que no se mezclen
        await ValidateOverlapAsync(dto);

        // 4. Mapear DTO a Entidad y Crear
        var schedule = new Schedule
        {
            CourseId = dto.CourseId,
            Day = dto.Day,
            StartHour = dto.StartHour,
            EndHour = dto.EndHour,
            ClassRoom = dto.ClassRoom,
            StudentsAmount = dto.StudentsAmount
        };

        return await _scheduleRepository.CreateAsync(schedule);
    }
    
    // Tarea: Actualizar un horario
    public async Task<bool> UpdateAsync(int scheduleId, ScheduleCreateUpdateDto dto)
    {
        // 1. Validar que el horario (Schedule) que quiere editar existe
        var existingSchedule = await _scheduleRepository.GetByIdAsync(scheduleId);
        if (existingSchedule == null) 
            return false; // No se encontró el horario

        // 2. Validar si el CourseId cambió
        if (existingSchedule.CourseId != dto.CourseId)
        {
            // Validar que el nuevo curso exista
            var newCourse = await _courseRepository.GetByIdAsync(dto.CourseId);
            if (newCourse == null)
                throw new KeyNotFoundException($"El nuevo curso con ID {dto.CourseId} no existe.");
            
            // Validar que el nuevo curso no tenga ya un horario (Lógica 1:1)
            var scheduleOnNewCourse = await _scheduleRepository.GetByCourseIdAsync(dto.CourseId);
            if (scheduleOnNewCourse != null)
            {
                throw new InvalidOperationException($"Conflicto: El nuevo curso '{newCourse.CourseName}' ya tiene un horario asignado.");
            }
        }
        
        // 3. Tarea: Validar solapamiento (excluyendo el horario actual)
        await ValidateOverlapAsync(dto, scheduleId);
        
        // 4. Mapear DTO a Entidad existente y Actualizar
        existingSchedule.CourseId = dto.CourseId;
        existingSchedule.Day = dto.Day;
        existingSchedule.StartHour = dto.StartHour;
        existingSchedule.EndHour = dto.EndHour;
        existingSchedule.ClassRoom = dto.ClassRoom;
        existingSchedule.StudentsAmount = dto.StudentsAmount;

        await _scheduleRepository.UpdateAsync(existingSchedule);
        return true;
    }
    
    // Tarea: Listar horario por curso
    public async Task<Schedule?> GetByCourseAsync(int courseId)
    {
        // Validar que el curso existe primero
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null) 
            throw new KeyNotFoundException($"El curso con ID {courseId} no existe.");

        // Devolver el horario (o null si no tiene uno)
        return await _scheduleRepository.GetByCourseIdAsync(courseId);
    }
    
    // (Helper) Tarea: Validar que los horarios no se solapen
    private async Task ValidateOverlapAsync(ScheduleCreateUpdateDto dto, int? scheduleIdToExclude = null)
    {
        // Validar horas lógicas
        if (dto.StartHour >= dto.EndHour)
        {
            throw new ArgumentException("La hora de inicio debe ser anterior a la hora de fin.");
        }
        
        var overlappingSchedules = await _scheduleRepository.GetOverlappingSchedulesAsync(
            dto.Day, dto.StartHour, dto.EndHour, dto.ClassRoom, scheduleIdToExclude);

        if (overlappingSchedules.Any())
        {
            throw new InvalidOperationException($"Conflicto: Ya existe un horario solapado en el aula '{dto.ClassRoom}' el día {dto.Day} en ese rango.");
        }
    }
    
    // (Helper) GetById para el controlador
    public async Task<Schedule?> GetByIdAsync(int id)
    {
        return await _scheduleRepository.GetByIdAsync(id);
    }
}