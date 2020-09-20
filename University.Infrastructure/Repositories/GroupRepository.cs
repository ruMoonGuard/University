using System;
using System.Threading.Tasks;
using University.Domain.Entities;
using University.Domain.Interfaces;

namespace University.Infrastructure.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        public Task<Guid> AddAsync(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<Group> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
