using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using University.Application.Commands.ChangeStudentUniqueName;
using University.Domain.Entities;
using University.Domain.Exceptions;
using University.Domain.Interfaces;
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

        [Theory, AutoData]
        public async Task ChangeStudentUniqueNameCommand_ChangeUniqueName_Successfully(ChangeStudentUniqueNameCommand command, Student student)
        {
            // Arrange
            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Student>()));

            var handler = new ChangeStudentUniqueNameCommandHandler(_mockStudentRepository.Object);

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
        public async Task ChangeStudentUniqueNameCommand_ChangeStudentWithExistingUniqueName_ThrowUniqueСonstraintException(ChangeStudentUniqueNameCommand command, Student student)
        {
            // Arrange
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
    }
}
