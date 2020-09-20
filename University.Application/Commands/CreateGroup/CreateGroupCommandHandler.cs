using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using University.Domain.Entities;
using University.Domain.Interfaces;

namespace University.Application.Commands.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly IGroupRepository _groupRepository;

        public CreateGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new Group(Guid.NewGuid(), request.Name);

            var id = await _groupRepository.AddAsync(group);

            return id;
        }
    }
}
