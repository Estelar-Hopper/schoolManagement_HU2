using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;
using gestionEscuela.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace gestionEscuela.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    
    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    // ------------------------------------------------------
    // INTERFACES TO IMPLEMENT:
    
    // GET BY ID:
    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.students_tb.FirstOrDefaultAsync(c => c.Id == id);
    }

    
    //GET ALL:
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.students_tb.ToListAsync();
    }
    

    // CREATE:
    public async Task<Student> CreateAsync(Student student)
    {
        try
        {
            _context.students_tb.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"It has presented an error. Error {ex.Message}");
            return null;
        }
    }

    
    // UPDATE:
    public async Task<Student?> UpdateAsync(Student student)
    {
        var existing = await _context.students_tb.FindAsync(student.Id);

        if (existing == null)
            return null;

        existing.Name = student.Name;
        existing.DocuNumber = student.DocuNumber;
        existing.Email = student.Name;
        existing.StudentCode = student.StudentCode;
        existing.Grade = student.Grade;

        await _context.SaveChangesAsync();
        return existing;
    }

    
    //DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var studentToDelete = await _context.students_tb.FindAsync(id);
        
        if (studentToDelete == null)
            return false;
        
        _context.students_tb.Remove(studentToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
    

}