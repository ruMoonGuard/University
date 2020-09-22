using MediatR;
using System.Threading;
using System.Threading.Tasks;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;

namespace University.Application.Commands.ChangeStudentUniqueName
{
    public class ChangeStudentUniqueNameCommandHandler : IRequestHandler<ChangeStudentUniqueNameCommand>
    {
        private IStudentRepository _studentRepository;

        public ChangeStudentUniqueNameCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Unit> Handle(ChangeStudentUniqueNameCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.UniqueName))
            {
                var isExist = await _studentRepository.IsExistByUniqueNameAsync(request.UniqueName);
                if (isExist)
                {
                    throw new UniqueСonstraintException($"UniqueName {request.UniqueName} already exists");
                }
            }

            var student = await _studentRepository.GetByIdAsync(request.Id);

            if (student == null)
            {
                throw new ObjectNotFoundException($"Student with id {request.Id} not found");
            }

            student.ChangeUniqueName(request.UniqueName);

            await _studentRepository.UpdateAsync(student);

            return Unit.Value;
        }
    }
}
