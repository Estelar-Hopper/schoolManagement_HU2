using gestionEscuela.Application.Dtos;
using gestionEscuela.Application.Services;
using gestionEscuela.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gestionEscuela.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }
    
    
    // ---------------------------------------------------
    
    
    // GET BY ID
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var grade = await _gradeService.GetByIdAsync(id);

        if (grade == null)
            return NotFound(new { message = $"Grade with ID {id} not found." });

        return Ok(grade);
    }


    // GET ALL:
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var grades = await _gradeService.GetAllAsync();

        return Ok(grades);
    }
    
    
    // CREATE:
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] GradeCreateUpdateDto gradeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var grade = new Grade
        {
            EnrollmentId = gradeDto.EnrollmentId,
            Score = gradeDto.Score
        };

        var createdGrade = await _gradeService.CreateAsync(grade);

        return CreatedAtAction(nameof(GetById), new { id = createdGrade.Id }, createdGrade);
    }
    
    
    // UPDATE:
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] GradeCreateUpdateDto gradeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingGrade = await _gradeService.GetByIdAsync(id);

        if (existingGrade == null)
            return NotFound(new { message = $"Register with ID {id} not found." });

        existingGrade.EnrollmentId = gradeDto.EnrollmentId;
        existingGrade.Score = gradeDto.Score;

        var updated = await _gradeService.UpdateAsync(existingGrade);

        if (!updated)
            return StatusCode(500, new { message = "Error updating." });

        return NoContent();
    }
    
    
    // DELETE:
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var gradeToDelete = await _gradeService.DeleteAsync(id);

        if (!gradeToDelete)
            return NotFound(new { message = $"Register with ID {id} not founded." });

        return NoContent();
    }
}
