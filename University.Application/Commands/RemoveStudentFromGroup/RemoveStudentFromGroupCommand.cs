using MediatR;
using System;

namespace University.Application.Commands.RemoveStudentFromGroup
{
    public class RemoveStudentFromGroupCommand : IRequest
    {
        public RemoveStudentFromGroupCommand(Guid studentId, Guid groupId)
        {
            if (studentId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(RemoveStudentFromGroupCommand)} studentId cannot empty!");
            }

            if (groupId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(RemoveStudentFromGroupCommand)}groupId cannot empty!");
            }

            StudentId = studentId;
            GroupId = groupId;
        }

        public Guid StudentId { get; }
        public Guid GroupId { get; }
    }
}
