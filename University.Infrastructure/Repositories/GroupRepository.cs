using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;

namespace University.Infrastructure.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly UniversityContext _context;

        public GroupRepository(UniversityContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return group.Id;
        }

        public async Task<Group> GetByIdAsync(Guid id) =>
            await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);

        public async Task RemoveAsync(Group group)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }
    }
}
