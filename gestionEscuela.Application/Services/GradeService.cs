using gestionEscuela.Domain.Entities;
using gestionEscuela.Domain.Repositories;

namespace gestionEscuela.Application.Services
{

    public class GradeService
    {
        private readonly IGenericRepository<Grade> _gradeRepository;

        public GradeService(IGenericRepository<Grade> gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        // -----------------------------------------------------------


        // GET BY ID
        public async Task<Grade> GetByIdAsync(int id)
        {
            return await _gradeRepository.GetByIdAsync(id);
        }


        // GET ALL
        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _gradeRepository.GetAllAsync();
        }


        // CREATE
        public async Task<Grade> CreateAsync(Grade grade)
        {
            return await _gradeRepository.CreateAsync(grade);
        }


        // UPDATE
        public async Task<bool> UpdateAsync(Grade grade)
        {
            var existing = await _gradeRepository.GetByIdAsync(grade.Id);

            if (existing == null)
                return false;

            await _gradeRepository.UpdateAsync(grade);
            return true;
        }


        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var gradeToDelete = await _gradeRepository.GetByIdAsync(id);

            if (gradeToDelete == null)
                return false;

            return await _gradeRepository.DeleteAsync(id);
        }
    }
}
