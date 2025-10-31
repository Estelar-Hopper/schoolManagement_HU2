using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;

namespace gestionEscuela.Tests.Fakes
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();

        public Task<Student?> GetByIdAsync(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(student);
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Student>>(_students);
        }

        public Task<Student> CreateAsync(Student student)
        {
            _students.Add(student);
            return Task.FromResult(student);
        }

        public Task<Student?> UpdateAsync(Student student)
        {
            var existing = _students.FirstOrDefault(s => s.Id == student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.StudentCode = student.StudentCode;
                existing.Email = student.Email;
                existing.Phone = student.Phone;
                existing.Enrollments = student.Enrollments;
                return Task.FromResult<Student?>(existing);
            }

            return Task.FromResult<Student?>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _students.Remove(student);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}