using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;

namespace University.Application.Commands.AddStudentToGroup
{
    public class AddStudentToGroupCommandHandler : IRequestHandler<AddStudentToGroupCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;

        public AddStudentToGroupCommandHandler(
            IStudentRepository studentRepository,
            IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(AddStudentToGroupCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new ObjectNotFoundException($"Student with id {request.StudentId} not found");
            }

            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group == null)
            {
                throw new ObjectNotFoundException($"Group with id {request.GroupId} not found");
            }

            student.AddGroup(group);
            await _studentRepository.UpdateAsync(student);

            return Unit.Value;
        }
    }
}
