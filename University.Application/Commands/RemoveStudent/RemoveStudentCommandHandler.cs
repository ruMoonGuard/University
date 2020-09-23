using MediatR;
using System.Threading;
using System.Threading.Tasks;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;

namespace University.Application.Commands.RemoveStudent
{
    public class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;

        public RemoveStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Unit> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new ObjectNotFoundException($"Student with id {request.StudentId} not found");
            }

            await _studentRepository.RemoveAsync(student);

            return Unit.Value;
        }
    }
}
