using gestionEscuela.Domain.Entities;

namespace gestionEscuela.Domain.Repositories;

public interface IEnrollmentRepository
{
    
    Task<Enrollment?> GetByIdAsync(int id);
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment> CreateAsync(Enrollment enrollment);
    Task<Enrollment?> UpdateAsync(Enrollment enrollment);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Course>> GetCoursesByStudentIdAsync(int studentId);
    Task<IEnumerable<Student>> GetStudentsByCourseIdAsync(int courseId);
}