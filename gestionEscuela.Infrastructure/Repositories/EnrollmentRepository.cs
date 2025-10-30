using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;
using gestionEscuela.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace gestionEscuela.Infrastructure.Repositories;

public class EnrollmentRepository : IGenericRepository<Enrollment>
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    
    // ----------------------------------------------------
    
    // INTERFACES TO IMPLEMENT:
    
    // GET BY ID:

    public async Task<Enrollment?> GetByIdAsync(int id)
    {
        return await _context.enrollments_tb.FirstOrDefaultAsync(e => e.Id == id);
    }

    
    // GET ALL:
    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _context.enrollments_tb.ToListAsync();
    }

    
    // CREATE:
    public async Task<Enrollment> CreateAsync(Enrollment enrollment)
    {
        try
        {
            _context.enrollments_tb.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"It has presented an error. Error {ex.Message}");
            return null;
        }
    }

    
    // UPDATE:
    public async Task<Enrollment?> UpdateAsync(Enrollment enrollment)
    {
        var existing = await _context.enrollments_tb.FindAsync(enrollment.Id);

        if (existing == null)
            return null;

        existing.StudentId = enrollment.StudentId;
        existing.CourseId = enrollment.CourseId;
        existing.InscriptionDate = enrollment.InscriptionDate;
        existing.Status = enrollment.Status;

        await _context.SaveChangesAsync();
        
        return enrollment;
    }

    
    // DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var enrollmentToDelete = await _context.enrollments_tb.FindAsync(id);

        if (enrollmentToDelete == null)
            return false;
        
        _context.enrollments_tb.Remove(enrollmentToDelete);
        await _context.SaveChangesAsync();

        return true;
    }
}