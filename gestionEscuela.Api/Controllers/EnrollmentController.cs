using gestionEscuela.Application.Dtos;
using gestionEscuela.Application.Services;
using gestionEscuela.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gestionEscuela.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }
    
    // ---------------------------------------------------

    // GET BY ID
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _enrollmentService.GetByIdAsync(id);

        if (course == null)
            return NotFound(new { message = $"Enrollment with ID {id} not found." });

        return Ok(course);
    }


    // GET ALL:
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await _enrollmentService.GetAllAsync();

        return Ok(enrollments);
    }
    
    
    // CREATE:
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] EnrollmentCreateUpdateDto enrollmentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var enrollment = new Enrollment
        {
            StudentId = enrollmentDto.StudentId,
            CourseId = enrollmentDto.CourseId,
            InscriptionDate = enrollmentDto.InscriptionDate,
            Status = enrollmentDto.Status,
        };

        var createdCourse = await _enrollmentService.CreateAsync(enrollment);

        return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
    }
    
    // TODO
    // UPDATE:
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] EnrollmentCreateUpdateDto enrollmentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingEnrollment = await _enrollmentService.GetByIdAsync(id);

        if (existingEnrollment == null)
            return NotFound(new { message = $"Register with ID {id} not found." });

        existingEnrollment.StudentId = enrollmentDto.StudentId;
        existingEnrollment.CourseId = enrollmentDto.CourseId;
        existingEnrollment.InscriptionDate = enrollmentDto.InscriptionDate;
        existingEnrollment.Status = enrollmentDto.Status;

        var updated = await _enrollmentService.UpdateAsync(existingEnrollment);

        if (!updated)
            return StatusCode(500, new { message = "Error updating." });

        return NoContent();

    }
    
    
    
    // DELETE:
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var enrollmentToDelete = await _enrollmentService.DeleteAsync(id);

        if (!enrollmentToDelete)
            return NotFound(new { message = $"Register with ID {id} not founded." });

        return NoContent();
    }
    
}