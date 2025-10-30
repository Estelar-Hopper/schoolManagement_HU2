using gestionEscuela.Domain.Entities;

namespace gestionEscuela.Domain.Repositories;

public interface IStudentRepository
{
  
    Task<Student?> GetByIdAsync(int id);
    Task<IEnumerable<Student>>GetAllAsync();
    Task<Student>CreateAsync(Student student);
    Task<Student?>UpdateAsync(Student student);
    Task<bool>DeleteAsync(int id);
    
}