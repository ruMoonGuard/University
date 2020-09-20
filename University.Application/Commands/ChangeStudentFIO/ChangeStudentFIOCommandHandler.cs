using MediatR;
using System.Threading;
using System.Threading.Tasks;
using University.Domain.Exceptions;
using University.Domain.Interfaces;

namespace University.Application.Commands.ChangeStudentFIO
{
    public class ChangeStudentFIOCommandHandler : IRequestHandler<ChangeStudentFIOCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public ChangeStudentFIOCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Unit> Handle(ChangeStudentFIOCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);

            if(student == null)
            {
                throw new ObjectNotFoundException($"Student with id {request.Id} not found");
            }

            student.ChangeFIO(request.FirstName, request.MiddleName, request.LastName);

            await _studentRepository.UpdateAsync(student);

            return Unit.Value;
        }
    }
}
