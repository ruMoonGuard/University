using MediatR;
using System;

namespace University.Application.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<Guid>
    {
        public CreateGroupCommand(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
