using MediatR;
using System;

namespace University.Application.Commands.ChangeGroupName
{
    public class ChangeGroupNameCommand : IRequest
    {
        public ChangeGroupNameCommand(Guid id, string updateName)
        {
            Id = id;
            UpdateName = updateName;
        }

        public Guid Id { get; }
        public string UpdateName { get; }
    }
}
