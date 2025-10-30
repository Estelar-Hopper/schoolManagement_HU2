using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;

namespace gestionEscuela.Application.Services;

public class EnrollmentService
{
    private readonly IGenericRepository<Enrollment> _enrollmentRepository;

    public EnrollmentService(IGenericRepository<Enrollment> enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    // -----------------------------------------------------------
    
    
    // GET BY ID
    public async Task<Enrollment> GetByIdAsync(int id)
    {
        return await _enrollmentRepository.GetByIdAsync(id);
    }
    
    
    // GET ALL
    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _enrollmentRepository.GetAllAsync();
    }
    
    
    // CREATE
    public async Task<Enrollment> CreateAsync(Enrollment enrollment)
    {
        return await _enrollmentRepository.CreateAsync(enrollment);
    }
    
    
    // UPDATE
    public async Task<bool> UpdateAsync(Enrollment enrollment)
    {
        var existing = await _enrollmentRepository.GetByIdAsync(enrollment.Id);

        if (existing == null)
            return false;
        
        await _enrollmentRepository.UpdateAsync(enrollment);

        return true;
    }
    
    
    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var enrollmentToDelete = await _enrollmentRepository.GetByIdAsync(id);

        if (enrollmentToDelete == null)
            return false;

        return await _enrollmentRepository.DeleteAsync(id);
    }
    
}