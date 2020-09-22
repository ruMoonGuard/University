using MediatR;
using System;

namespace University.Application.Commands.ChangeStudentUniqueName
{
    public class ChangeStudentUniqueNameCommand : IRequest
    {
        public ChangeStudentUniqueNameCommand(Guid id, string name)
        {
            Id = id;
            UniqueName = name;
        }

        public Guid Id { get; }
        public string UniqueName { get; }
    }
}
