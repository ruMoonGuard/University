using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;

namespace University.Infrastructure.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityContext _context;

        public StudentRepository(UniversityContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student.Id;
        }

        public async Task<Student> GetByIdAsync(Guid id) =>
            await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<bool> IsExistByUniqueNameAsync(string name) =>
            await _context.Students.AllAsync(s => s.UniqueName == name);

        public async Task RemoveAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
