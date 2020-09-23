using System;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Application.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsync(Guid id);
        Task<bool> IsExistByUniqueNameAsync(string name);
        Task<Guid> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task RemoveAsync(Guid id);
    }
}
