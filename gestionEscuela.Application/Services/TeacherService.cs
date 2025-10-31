using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;

namespace gestionEscuela.Application.Services;

public class TeacherService
{
    private readonly IGenericRepository<Teacher> _teacherRepository;

    public TeacherService(IGenericRepository<Teacher> teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    
    //-----------------------------------------------------------------------
    //GET BY ID:
    public async Task<Teacher?> GetByIdAsync(int id)
    {
        return await _teacherRepository.GetByIdAsync(id);
    }
    
    //GET ALL:
    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _teacherRepository.GetAllAsync();
    }
    
    //CREATE:
    public async Task<Teacher> CreateAsync(Teacher teacher)
    {
        return await _teacherRepository.CreateAsync(teacher);
    }
    
    //UPDATE:
    public async Task<bool> UpdateAsync(Teacher teacher)
    {
        var existing = await _teacherRepository.GetByIdAsync(teacher.Id);
        if (existing == null)
            return false;

        await _teacherRepository.UpdateAsync(teacher);
        return true;
    }
    
    //DELETE:
    public async Task<bool> DeleteAsync (int id)
    {
        var existing = await _teacherRepository.GetByIdAsync(id);
        if (existing == null)
            return false;

        return await _teacherRepository.DeleteAsync(id);
    }



}