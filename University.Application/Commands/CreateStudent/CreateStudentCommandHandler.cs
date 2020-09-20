using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using University.Domain.Entities;
using University.Domain.Exceptions;
using University.Domain.Interfaces;

namespace University.Application.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            if(!string.IsNullOrEmpty(request.UniqueName))
            {
                var isExist = await _studentRepository.IsExistByUniqueNameAsync(request.UniqueName);
                if (isExist)
                {
                    throw new UniqueСonstraintException($"UniqueName {request.UniqueName} already exists");
                }
            }

            var student = new Student(Guid.NewGuid(), request.Gender, request.FirstName, request.LastName, request.MiddleName, request.UniqueName);

            var id = await _studentRepository.AddAsync(student);

            return id; 
        }
    }
}
