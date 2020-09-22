using MediatR;
using System;

namespace University.Application.Commands.AddStudentToGroup
{
    public class AddStudentToGroupCommand : IRequest
    {
        public AddStudentToGroupCommand(Guid studentId, Guid groupId)
        {
            if(studentId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(AddStudentToGroupCommand)} studentId cannot empty!");
            }

            if(groupId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(AddStudentToGroupCommand)}groupId cannot empty!");
            }

            StudentId = studentId;
            GroupId = groupId;
        }

        public Guid StudentId { get; }
        public Guid GroupId { get; }
    }
}
