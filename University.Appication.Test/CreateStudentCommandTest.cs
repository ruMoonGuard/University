using AutoFixture.Xunit2;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using University.Appication.Test.Helpers;
using University.Application.Commands.CreateStudent;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;
using Xunit;

namespace University.Appication.Test
{
    public class CreateStudentCommandTest
    {
        private readonly Mock<IStudentRepository> _mockStudentRepository;

        public CreateStudentCommandTest()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
        }

        [Fact]
        public async Task CreateStudentCommand_CreatesStudent_Successfully()
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();
            var command = new CreateStudentCommand(student.Gender, student.LastName, student.FirstName, student.MiddleName, student.UniqueName);

            _mockStudentRepository
                .Setup(m => m.IsExistByUniqueNameAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _mockStudentRepository
                .Setup(m => m.AddAsync(It.IsAny<Student>()))
                .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateStudentCommandHandler(_mockStudentRepository.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            _mockStudentRepository.Verify(m => m.AddAsync(It.IsAny<Student>()), Times.Once());
        }

        [Fact]
        public async Task CreateStudentCommand_CreatesStudentWithUniqueName_Successfully()
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();
            var command = new CreateStudentCommand(student.Gender, student.LastName, student.FirstName, student.MiddleName, student.UniqueName);

            _mockStudentRepository
                .Setup(m => m.IsExistByUniqueNameAsync(command.UniqueName))
                .ReturnsAsync(false);

            _mockStudentRepository
                .Setup(m => m.AddAsync(It.IsAny<Student>()))
                .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateStudentCommandHandler(_mockStudentRepository.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            _mockStudentRepository.Verify(m => m.IsExistByUniqueNameAsync(command.UniqueName), Times.Once());
        }

        [Theory, AutoData]
        public async Task CreateStudentCommand_CreatesStudentWithExistingUniqueName_ThrowUniqueСonstraintException(CreateStudentCommand command)
        {
            // Arrange
            _mockStudentRepository
                .Setup(m => m.IsExistByUniqueNameAsync(command.UniqueName))
                .ReturnsAsync(true);

            _mockStudentRepository
                .Setup(m => m.AddAsync(It.IsAny<Student>()))
                .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateStudentCommandHandler(_mockStudentRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<UniqueСonstraintException>(action);
        }

        [Fact]
        public async Task CreateStudentCommand_CreatesStudentWithEmptyFirstName_ThrowArgumentNullException()
        {
            // Arrange
            var command = new CreateStudentCommand(Domain.Entities.Enums.Gender.Female, "Depp", "");

            _mockStudentRepository
                .Setup(m => m.IsExistByUniqueNameAsync(command.UniqueName))
                .ReturnsAsync(false);

            _mockStudentRepository
                .Setup(m => m.AddAsync(It.IsAny<Student>()))
                .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateStudentCommandHandler(_mockStudentRepository.Object);

            // act 
            Func<Task> action = async () => await handler.Handle(command, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task CreateStudentCommand_CreatesStudentWithEmptyLastName_ThrowArgumentNullException()
        {
            // Arrange
            var command = new CreateStudentCommand(Domain.Entities.Enums.Gender.Female, "", "John");

            _mockStudentRepository
                .Setup(m => m.IsExistByUniqueNameAsync(command.UniqueName))
                .ReturnsAsync(false);

            _mockStudentRepository
                .Setup(m => m.AddAsync(It.IsAny<Student>()))
                .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateStudentCommandHandler(_mockStudentRepository.Object);

            // act 
            Func<Task> action = async () => await handler.Handle(command, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }
    }
}
