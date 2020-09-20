using MediatR;
using System;
using University.Domain.Entities.Enums;

namespace University.Application.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<Guid>
    {
        public CreateStudentCommand(Gender gender, string lastName, string firstName, string middleName = null, string uniqueName = null)
        {
            Gender = gender;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            UniqueName = uniqueName;
        }

        public Gender Gender { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string UniqueName { get; private set; }
    }
}
