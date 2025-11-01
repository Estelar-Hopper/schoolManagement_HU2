using gestionEscuela.Application.Dtos;
using gestionEscuela.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace gestionEscuela.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _scheduleService;

    public ScheduleController(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    // Tarea: Listar horario por curso
    [HttpGet("byCourse/{courseId:int}")]
    public async Task<IActionResult> GetByCourse(int courseId)
    {
        try
        {
            var schedule = await _scheduleService.GetByCourseAsync(courseId);
            if (schedule == null)
                return NotFound(new { message = $"El curso con ID {courseId} no tiene un horario asignado." }); 
            
            return Ok(schedule);
        }
        catch (KeyNotFoundException ex) // El curso no existe
        {
            return NotFound(new { message = ex.Message }); // 404
        }
    }

    // Tarea: Crear horario
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ScheduleCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var createdSchedule = await _scheduleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdSchedule.Id }, createdSchedule);
        }
        catch (KeyNotFoundException ex) // Curso no encontrado
        {
            return NotFound(new { message = ex.Message }); // 404
        }
        catch (ArgumentException ex) // Horas inválidas
        {
            return BadRequest(new { message = ex.Message }); // 400
        }
        catch (InvalidOperationException ex) // Conflicto 1:1 o Solapamiento
        {
            return Conflict(new { message = ex.Message }); // 409
        }
    }

    // Tarea: Actualizar horario
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ScheduleCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await _scheduleService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { message = $"Horario con ID {id} no encontrado." }); // 404
            
            return NoContent(); // 204 (Éxito)
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message }); // 404
        }
        catch (ArgumentException ex) // Horas inválidas
        {
            return BadRequest(new { message = ex.Message }); // 400
        }
        catch (InvalidOperationException ex) // Conflicto 1:1 
        {
            return Conflict(new { message = ex.Message }); // 409
        }
    }

    // (Helper) GetById para que CreatedAtAction funcione
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var schedule = await _scheduleService.GetByIdAsync(id);
        if (schedule == null)
            return NotFound(new { message = $"Horario con ID {id} no encontrado." });
        
        return Ok(schedule);
    }
}