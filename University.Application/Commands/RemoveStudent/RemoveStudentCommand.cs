using MediatR;
using System;

namespace University.Application.Commands.RemoveStudent
{
    public class RemoveStudentCommand : IRequest
    {
        public RemoveStudentCommand(Guid studentId)
        {
            StudentId = studentId != Guid.Empty ? studentId : throw new ArgumentException($"{nameof(RemoveStudentCommand)} cannot studentId empty!");
        }

        public Guid StudentId { get; }
    }
}
