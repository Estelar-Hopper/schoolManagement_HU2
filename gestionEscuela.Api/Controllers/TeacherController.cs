using gestionEscuela.Application.Dtos;
using gestionEscuela.Application.Services;
using gestionEscuela.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace gestionEscuela.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        //----------------------------------------------------------

        // GET BY ID
        [HttpGet("getById/{id:int}")]

        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);

            if (teacher == null)
                return NotFound(new { message = $"Teacher whit ID {id} not found." });

            return Ok(teacher);
        }

        // GET ALL:
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            return Ok(teachers);
        }

        // CREATE:
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TeacherCreateUpdateDto teacherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = new Teacher
            {
                Name = teacherDto.Name,
                DocuNumber = teacherDto.DocuNumber,
                Email = teacherDto.Email,
                Phone = teacherDto.Phone,
                TeacherCode = teacherDto.TeacherCode,
                Career = teacherDto.Career
            };

            var createdTeacher = await _teacherService.CreateAsync(teacher);

            return CreatedAtAction(nameof(GetById), new { id = createdTeacher.Id }, createdTeacher);
        }

        // UPDATE
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherCreateUpdateDto teacherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTeacher = await _teacherService.GetByIdAsync(id);
            if (existingTeacher == null)
                return NotFound(new { message = $"Student with ID {id} not found." });

            existingTeacher.Name = teacherDto.Name;
            existingTeacher.DocuNumber = teacherDto.DocuNumber;
            existingTeacher.Email = teacherDto.Email;
            existingTeacher.Phone = teacherDto.Phone;
            existingTeacher.TeacherCode = teacherDto.TeacherCode;
            existingTeacher.Career = teacherDto.Career;

            var updated = await _teacherService.UpdateAsync(existingTeacher);

            if (!updated)
                return StatusCode(500, new { message = "Error updating student." });

            return NoContent();
        }

        // DELETE:
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var teacherDelete = await _teacherService.DeleteAsync(id);

            if (!teacherDelete)
                return NotFound(new { message = $"Student with ID {id} not founded." });
            return NoContent();
        }

    }
}
