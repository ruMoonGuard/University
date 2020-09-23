using MediatR;
using System.Threading;
using System.Threading.Tasks;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;

namespace University.Application.Commands.RemoveGroup
{
    public class RemoveGroupCommandHandler : IRequestHandler<RemoveGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;

        public RemoveGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group == null)
            {
                throw new ObjectNotFoundException($"Group with id {request.GroupId} not found");
            }

            await _groupRepository.RemoveAsync(group);

            return Unit.Value;
        }
    }
}
