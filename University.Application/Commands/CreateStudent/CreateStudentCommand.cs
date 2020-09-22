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

        public Gender Gender { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string UniqueName { get; }
    }
}
