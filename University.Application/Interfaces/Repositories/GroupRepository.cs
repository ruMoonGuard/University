using System;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Application.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<Group> FindByIdAsync(Guid id);
        Task<Guid> AddAsync(Group group);
        Task UpdateAsync(Group group);
    }
}
