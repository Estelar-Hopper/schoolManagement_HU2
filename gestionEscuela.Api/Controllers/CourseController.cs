using gestionEscuela.Application.Dtos;
using gestionEscuela.Application.Services;
using gestionEscuela.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gestionEscuela.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }
    
    // ---------------------------------------------------

    // GET BY ID
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _courseService.GetByIdAsync(id);

        if (course == null)
            return NotFound(new { message = $"Course with ID {id} not found." });

        return Ok(course);
    }


    // GET ALL:
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _courseService.GetAllAsync();

        return Ok(courses);
    }
    
    
    // CREATE:
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CourseCreateUpdateDto courseDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var course = new Course
        {
            CourseName = courseDto.CourseName,
            CourseCode = courseDto.CourseCode,
            TeacherId = courseDto.TeacherId,
            ClassRoom = courseDto.ClassRoom,
            CourseActive = courseDto.CourseActive
        };

        var createdCourse = await _courseService.CreateAsync(course);

        return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
    }
    
    
    // UPDATE:
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CourseCreateUpdateDto courseDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingCourse = await _courseService.GetByIdAsync(id);

        if (existingCourse == null)
            return NotFound(new { message = $"Register with ID {id} not found." });

        existingCourse.CourseName = courseDto.CourseName;
        existingCourse.CourseCode = courseDto.CourseCode;
        existingCourse.TeacherId = courseDto.TeacherId;
        existingCourse.ClassRoom = courseDto.ClassRoom;
        existingCourse.CourseActive = courseDto.CourseActive;

        var updated = await _courseService.UpdateAsync(existingCourse);

        if (!updated)
            return StatusCode(500, new { message = "Error updating." });

        return NoContent();

    }
    
    
    
    // DELETE:
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var courseToDelete = await _courseService.DeleteAsync(id);

        if (!courseToDelete)
            return NotFound(new { message = $"Register with ID {id} not founded." });

        return NoContent();
    }

}