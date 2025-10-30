using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;

namespace gestionEscuela.Application.Services;

public class StudentService 
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    // -----------------------------------------------------------

    //GET BY ID:
    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    
    //GET ALL:
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    
    //CREATE:
    public async Task<Student> CreateAsync(Student student)
    {
        return await _studentRepository.CreateAsync(student);
    }

    
    //UPDATE:
    public async Task<bool> UpdateAsync(Student student)
    {
        var existing = await _studentRepository.GetByIdAsync(student.Id);
        if (existing == null)
            return false;

        await _studentRepository.UpdateAsync(student);
        return true;
    }

    
    //DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _studentRepository.GetByIdAsync(id);
        if (existing == null)
            return false;

        return await _studentRepository.DeleteAsync(id);
    }
}