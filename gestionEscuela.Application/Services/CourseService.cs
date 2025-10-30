using System.Net;
using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;

namespace gestionEscuela.Application.Services;

public class CourseService
{
    private readonly IGenericRepository<Course> _courseRepository;

    public CourseService(IGenericRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }
    
    // -----------------------------------------------------------
    
    
    // GET BY ID
    public async Task<Course> GetByIdAsync(int id)
    {
        return await _courseRepository.GetByIdAsync(id);
    }
    
    
    // GET ALL
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _courseRepository.GetAllAsync();
    }
    
    
    // CREATE
    public async Task<Course> CreateAsync(Course course)
    {
        return await _courseRepository.CreateAsync(course);
    }
    
    
    // UPDATE
    public async Task<bool> UpdateAsync(Course course)
    {
        var existing = await _courseRepository.GetByIdAsync(course.Id);

        if (existing == null)
            return false;
        
        await _courseRepository.UpdateAsync(course);

        return true;
    }
    
    
    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var courseToDelete = await _courseRepository.GetByIdAsync(id);

        if (courseToDelete == null)
            return false;

        return await _courseRepository.DeleteAsync(id);
    }
    
}