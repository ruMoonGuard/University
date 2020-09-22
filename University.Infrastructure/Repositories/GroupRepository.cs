using System;
using System.Threading.Tasks;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;

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
