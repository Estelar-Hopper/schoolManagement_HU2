using gestionEscuela.Domain.Entities;

namespace gestionEscuela.Domain.Repositories;

public interface IGenericRepository<T>
{
    //I created this general IRepository for optimization of code.

    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}