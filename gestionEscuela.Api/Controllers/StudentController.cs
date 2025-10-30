using gestionEscuela.Application.Dtos;
using gestionEscuela.Application.Services;
using gestionEscuela.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gestionEscuela.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }
    
    //--------------------------------------------------------------

    // GET BY ID
    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);

        if (student == null)
            return NotFound(new { message = $"Student with ID {id} not found." });

        return Ok(student);
    }

    
    //GET ALL:
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAllAsync();

        return Ok(students);
    }

    
    //CREATE
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] StudentCreateUpdateDto studentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = new Student
        {
            Name = studentDto.Name,
            DocuNumber = studentDto.DocNumber,
            Email = studentDto.Email,
            Phone = studentDto.Phone,
            StudentCode = studentDto.StudentCode,
            Grade = studentDto.Grade
        };
        
        var createdStudent = await _studentService.CreateAsync(student);
        
        return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
    }
    
    
    //UPDATE
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentCreateUpdateDto studentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingStudent = await _studentService.GetByIdAsync(id);
        if (existingStudent == null)
            return NotFound(new { message = $"Student with ID {id} not found." });

        existingStudent.Name = studentDto.Name;
        existingStudent.DocuNumber = studentDto.DocNumber;
        existingStudent.Email = studentDto.Email;
        existingStudent.Phone = studentDto.Phone;
        existingStudent.StudentCode = studentDto.StudentCode;
        existingStudent.Grade = studentDto.Grade;
        

        var updated = await _studentService.UpdateAsync(existingStudent);

        
        if (!updated)
            return StatusCode(500, new { message = "Error updating student." });

        return NoContent();
    }
    
    
    
    //DELETE
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var studentToDelete = await _studentService.DeleteAsync(id);

        if (!studentToDelete)
            return NotFound(new { message = $"Student with ID {id} not founded." });

        return NoContent();
    }
    
    
    
}