using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Appication.Test.Helpers;
using University.Application.Commands.AddStudentToGroup;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;
using Xunit;

namespace University.Appication.Test
{
    public class AddStudentToGroupCommandTest
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<IGroupRepository> _groupRepository;

        public AddStudentToGroupCommandTest()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _groupRepository = new Mock<IGroupRepository>();
        }

        [Theory, AutoData]
        public async Task AddStudentToGroupCommandTest_AddStudentToGroup_Successfully(Group group)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _studentRepository.Setup(m => m.UpdateAsync(student));

            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(group);

            var command = new AddStudentToGroupCommand(student.Id, group.Id);
            var handle = new AddStudentToGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            _studentRepository.Verify(m => m.UpdateAsync(student));
            Assert.Contains(student.StudentGroup, s => s.GroupId == command.GroupId);
        }

        [Theory, AutoData]
        public async Task AddStudentToGroupCommandTest_NotFoundStudent_ThrowObjectNotFoundException(AddStudentToGroupCommand command, Group group)
        {
            // Arrange
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Student)null);
            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(group);

            var handle = new AddStudentToGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            Func<Task> action = async () => await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(action);
        }

        [Theory, AutoData]
        public async Task AddStudentToGroupCommandTest_NotFoundGroup_ThrowObjectNotFoundException(AddStudentToGroupCommand command)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Group)null);

            var handle = new AddStudentToGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            Func<Task> action = async () => await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(action);
        }

        [Theory, AutoData]
        public async Task AddStudentToGroupCommandTest_AddDublicateGroup_ThrowArgumentException(Group group)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent(group: group);
            _studentRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _studentRepository.Setup(m => m.UpdateAsync(student));

            _groupRepository.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(group);

            var command = new AddStudentToGroupCommand(student.Id, group.Id);
            var handle = new AddStudentToGroupCommandHandler(_studentRepository.Object, _groupRepository.Object);

            // Act
            Func<Task> action = async () => await handle.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(action);
        }
    }
}
