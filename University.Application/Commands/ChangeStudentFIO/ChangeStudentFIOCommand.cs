using MediatR;
using System;

namespace University.Application.Commands.ChangeStudentFIO
{
    public class ChangeStudentFIOCommand : IRequest
    {
        public ChangeStudentFIOCommand(Guid id, string firstname, string lastName, string middleName)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastName;
            MiddleName = middleName;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
    }
}
