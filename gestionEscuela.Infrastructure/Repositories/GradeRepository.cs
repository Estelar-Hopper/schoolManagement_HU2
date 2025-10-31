using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;
using gestionEscuela.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace gestionEscuela.Infrastructure.Repositories
{

    public class GradeRepository : IGenericRepository<Grade>
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context)
        {
            _context = context;
        }

        // ------------------------------------------------------

        // INTERFACES TO IMPLEMENT:

        //GET BY ID:
        public async Task<Grade?> GetByIdAsync(int id)
        {
            return await _context.grades_tb.FirstOrDefaultAsync(g => g.Id == id);
        }


        //GET ALL:
        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _context.grades_tb.ToListAsync();
        }


        //CREATE:
        public async Task<Grade> CreateAsync(Grade grade)
        {
            try
            {
                _context.grades_tb.Add(grade);
                await _context.SaveChangesAsync();
                return grade;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"It has presented an error. Error {ex.Message}");
                throw;
            }
        }


        //UPDATE:
        public async Task<Grade?> UpdateAsync(Grade grade)
        {
            var existing = await _context.grades_tb.FindAsync(grade.Id);

            if (existing == null)
                return null;

            existing.EnrollmentId = grade.EnrollmentId;
            existing.Score = grade.Score;

            await _context.SaveChangesAsync();

            return grade;
        }


        //DELETE:
        public async Task<bool> DeleteAsync(int id)
        {
            var gradeToDelete = await _context.grades_tb.FindAsync(id);

            if (gradeToDelete == null)
                return false;

            _context.grades_tb.Remove(gradeToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}