using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using University.Appication.Test.Builders;
using University.Appication.Test.Helpers;
using University.Application.Commands.ChangeStudentUniqueName;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;
using Xunit;

namespace University.Appication.Test
{
    public class ChangeStudentUniqueNameCommandTest
    {
        private readonly Mock<IStudentRepository> _mockStudentRepository;

        public ChangeStudentUniqueNameCommandTest()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
        }

        [Fact]
        public async Task ChangeStudentUniqueNameCommand_ChangeUniqueName_Successfully()
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();

            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Student>()));

            var handler = new ChangeStudentUniqueNameCommandHandler(_mockStudentRepository.Object);
            var command = new ChangeStudentUniqueNameCommand(student.Id, student.UniqueName);

            // Act
            await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            _mockStudentRepository.Verify(m => m.UpdateAsync(It.IsAny<Student>()), Times.Once());
        }

        [Theory, AutoData]
        public async Task ChangeStudentUniqueNameCommand_NotFoundStudent_ThrowObjectNotFoundException(ChangeStudentUniqueNameCommand command)
        {
            // Arrange
            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(command.Id))
                .ReturnsAsync((Student)null);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Student>()));

            var handler = new ChangeStudentUniqueNameCommandHandler(_mockStudentRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(action);
        }

        [Theory, AutoData]
        public async Task ChangeStudentUniqueNameCommand_ChangeStudentWithExistingUniqueName_ThrowUniqueСonstraintException(ChangeStudentUniqueNameCommand command)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();

            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

            _mockStudentRepository
                .Setup(m => m.IsExistByUniqueNameAsync(command.UniqueName))
                .ReturnsAsync(true);

            var handler = new ChangeStudentUniqueNameCommandHandler(_mockStudentRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<UniqueСonstraintException>(action);
        }

        [Theory]
        [InlineAutoData("12345")]
        [InlineAutoData("123456789abcdefgh")]
        public async Task ChangeStudentUniqueNameCommand_ChangeUniqueNameWithIncorrectLength_ThrowArgumentException(string uniqueName)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();

            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Student>()));

            var command = new ChangeStudentUniqueNameCommand(student.Id, uniqueName);
            var handler = new ChangeStudentUniqueNameCommandHandler(_mockStudentRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(action);
        }
    }
}
