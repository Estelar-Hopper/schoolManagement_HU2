using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;
using gestionEscuela.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace gestionEscuela.Infrastructure.Repositories;

public class TeacherRepository : IGenericRepository<Teacher>
{
    private readonly AppDbContext _context;

    public TeacherRepository(AppDbContext context)
    {
        _context = context;
    }
    //----------------------------------------------
    // INTERFACES TO IMPLEMENT:
    
    //GET BY ID:
    public async Task<Teacher?> GetByIdAsync(int id)
    {
        return await _context.teachers_tb.FirstOrDefaultAsync(c => c.Id == id);
    }
    
    //GET ALL:
    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.teachers_tb.ToListAsync();
    }
    
    //CREATE:
    public async Task<Teacher> CreateAsync(Teacher teacher)
    {
        try
        {
            _context.teachers_tb.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"It has presented an error. Error{exception.Message}");
            throw;
        }
    }
    
    // UPDATE:
    public async Task<Teacher?> UpdateAsync(Teacher teacher)
    {
        var existing = await _context.teachers_tb.FindAsync(teacher.Id);

        if (existing == null)
            return null;

        existing.Name = teacher.Name;
        existing.DocuNumber = teacher.DocuNumber;
        existing.Email = teacher.Email;
        existing.Phone = teacher.Phone;
        existing.TeacherCode = teacher.TeacherCode;
        existing.Career = teacher.Career;
        
        await _context.SaveChangesAsync();
        return existing;
    }
    
    //DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var teacherToDelete = await _context.teachers_tb.FindAsync(id);

        if (teacherToDelete == null)
            return false;

        _context.teachers_tb.Remove(teacherToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}
