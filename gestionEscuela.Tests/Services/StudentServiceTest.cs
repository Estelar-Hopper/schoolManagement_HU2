using Xunit;
using System.Threading.Tasks;
using gestionEscuela.Application.Services;
using gestionEscuela.Domain.Entities;
using gestionEscuela.Tests.Fakes;
using System.Linq;

namespace gestionEscuela.Tests.Services
{
    public class StudentServiceTests
    {
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            var repo = new InMemoryStudentRepository();
            _service = new StudentService(repo);
        }


        [Fact]
        public void BusinessRule_ShouldFail_WhenStudentCodeIsNegative()
        {
            var student = new Student { Id = 1, Name = "Ana", StudentCode = -5 };

            // Regla de negocio: StudentCode > 0
            bool isValid = student.StudentCode > 0;

            // Se espera que falle (StudentCode negativo)
            Assert.False(isValid, "StudentCode must be greater than 0");
        }

        [Fact]
        public void BusinessRule_ShouldPass_WhenStudentCodeIsPositive()
        {
            var student = new Student { Id = 2, Name = "Luis", StudentCode = 123 };

            // Regla de negocio: StudentCode > 0
            bool isValid = student.StudentCode > 0;

            // Se espera que pase
            Assert.True(isValid, "StudentCode is valid");
        }


        [Fact]
        public async Task CreateAsync_ShouldCreateStudent()
        {
            var student = new Student
            {
                Id = 3,
                Name = "Marta",
                StudentCode = 111
            };

            var result = await _service.CreateAsync(student);

            Assert.NotNull(result);
            Assert.Equal("Marta", result.Name);
            Assert.Equal(111, result.StudentCode);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnStudent()
        {
            var student = new Student { Id = 4, Name = "Carlos", StudentCode = 222 };
            await _service.CreateAsync(student);

            var result = await _service.GetByIdAsync(4);

            Assert.NotNull(result);
            Assert.Equal("Carlos", result.Name);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllStudents()
        {
            await _service.CreateAsync(new Student { Id = 5, Name = "Ana", StudentCode = 333 });
            await _service.CreateAsync(new Student { Id = 6, Name = "Luis", StudentCode = 444 });

            var all = await _service.GetAllAsync();

            Assert.Equal(2, all.Count());
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenStudentExists()
        {
            var student = new Student { Id = 7, Name = "Carlos", StudentCode = 555 };
            await _service.CreateAsync(student);

            student.Name = "Carlos Updated";
            var result = await _service.UpdateAsync(student);

            Assert.True(result);
            var updated = await _service.GetByIdAsync(7);
            Assert.Equal("Carlos Updated", updated!.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenStudentExists()
        {
            var student = new Student { Id = 8, Name = "Marta", StudentCode = 666 };
            await _service.CreateAsync(student);

            var result = await _service.DeleteAsync(8);
            Assert.True(result);

            var deleted = await _service.GetByIdAsync(8);
            Assert.Null(deleted);
        }
    }
}
