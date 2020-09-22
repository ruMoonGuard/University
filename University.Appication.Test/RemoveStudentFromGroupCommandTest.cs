using AutoFixture.Xunit2;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using University.Appication.Test.Helpers;
using University.Application.Commands.RemoveStudentFromGroup;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;
using Xunit;

namespace University.Appication.Test
{
    public class RemoveStudentFromGroupCommandTest
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<IGroupRepository> _groupRepository;

        public RemoveStudentFromGroupCommandTest()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _groupRepository = new Mock<IGroupRepository>();
        }

        [Theory, AutoData]
        public async Task RemoveStudentFromGroupCommand_RemoveStudentToGroup_Successfully(Group group)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent(group: group);
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _studentRepository.Setup(m => m.UpdateAsync(student));

            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(group);

            var command = new RemoveStudentFromGroupCommand(student.Id, group.Id);
            var handle = new RemoveStudentFromGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            _studentRepository.Verify(m => m.UpdateAsync(student));
            Assert.Empty(student.StudentGroup.Where(m => m.GroupId == group.Id));
        }

        [Theory, AutoData]
        public async Task RemoveStudentFromGroupCommand_NotFoundStudent_ThrowObjectNotFoundException(RemoveStudentFromGroupCommand command, Group group)
        {
            // Arrange
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Student)null);
            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(group);

            var handle = new RemoveStudentFromGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            Func<Task> action = async () => await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(action);
        }

        [Theory, AutoData]
        public async Task RemoveStudentFromGroupCommand_NotFoundGroup_ThrowObjectNotFoundException(RemoveStudentFromGroupCommand command)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Group)null);

            var handle = new RemoveStudentFromGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            Func<Task> action = async () => await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(action);
        }

        [Theory, AutoData]
        public async Task RemoveStudentFromGroupCommand_RemoveFromNonExistentGroup_ThrowArgumentException(Group group)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _studentRepository.Setup(m => m.UpdateAsync(student));

            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(group);

            var command = new RemoveStudentFromGroupCommand(student.Id, group.Id);
            var handle = new RemoveStudentFromGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            Func<Task> action = async () => await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(action);
        }
    }
}
