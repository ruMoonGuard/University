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

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
    }
}
