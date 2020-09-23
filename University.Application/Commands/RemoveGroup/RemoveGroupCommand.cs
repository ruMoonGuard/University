using MediatR;
using System;

namespace University.Application.Commands.RemoveGroup
{
    public class RemoveGroupCommand : IRequest
    {
        public RemoveGroupCommand(Guid groupId)
        {
            GroupId = groupId != Guid.Empty ? groupId : throw new ArgumentException($"{nameof(RemoveGroupCommand)} cannot empty groupId!");
        }

        public Guid GroupId { get; }
    }
}
