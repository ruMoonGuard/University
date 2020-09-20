using MediatR;
using System.Threading;
using System.Threading.Tasks;
using University.Domain.Exceptions;
using University.Domain.Interfaces;

namespace University.Application.Commands.ChangeGroupName
{
    public class ChangeGroupNameCommandHandler : IRequestHandler<ChangeGroupNameCommand>
    {
        private readonly IGroupRepository _groupRepository;

        public ChangeGroupNameCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(ChangeGroupNameCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.FindByIdAsync(request.Id);

            if(group == null)
            {
                throw new ObjectNotFoundException($"group with id {request.Id} not found");
            }

            group.ChangeName(request.UpdateName);

            await _groupRepository.UpdateAsync(group);

            return Unit.Value;
        }
    }
}
