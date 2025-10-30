using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;
using gestionEscuela.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace gestionEscuela.Infrastructure.Repositories;

public class CourseRepository : IGenericRepository<Course>
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }
    
    // ------------------------------------------------------
    
    // INTERFACES TO IMPLEMENT:
    
    //GET BY ID:
    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.courses_tb.FirstOrDefaultAsync(c => c.Id == id);
    }

    
    //GET ALL:
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.courses_tb.ToListAsync();
    }

    
    //CREATE:
    public async Task<Course> CreateAsync(Course course)
    {
        try
        {
            _context.courses_tb.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"It has presented an error. Error {ex.Message}");
            return null;
        }
    }

    
    //UPDATE:
    public async Task<Course?> UpdateAsync(Course course)
    {
        var existing = await _context.courses_tb.FindAsync(course.Id);

        if (existing == null)
            return null;

        existing.CourseName = course.CourseName;
        existing.CourseCode = course.CourseCode;
        existing.TeacherId = course.TeacherId;
        existing.ClassRoom = course.ClassRoom;
        existing.CourseActive = course.CourseActive;

        await _context.SaveChangesAsync();
        
        return course;
    }

    
    //DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var courseToDelete = await _context.courses_tb.FindAsync(id);

        if (courseToDelete == null)
            return false;
        
        _context.courses_tb.Remove(courseToDelete);
        await _context.SaveChangesAsync();

        return true;
    }
}