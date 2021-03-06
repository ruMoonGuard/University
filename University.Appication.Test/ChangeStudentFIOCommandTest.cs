﻿using AutoFixture.Xunit2;
using Moq;
using System;
using System.Threading.Tasks;
using University.Appication.Test.Helpers;
using University.Application.Commands.ChangeStudentFIO;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;
using Xunit;

namespace University.Appication.Test
{
    public class ChangeStudentFIOCommandTest
    {
        private readonly Mock<IStudentRepository> _mockStudentRepository;

        public ChangeStudentFIOCommandTest()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
        }

        [Theory, AutoData]
        public async Task ChangeFIOForStudentCommand_ChangeFIO_Successfully(ChangeStudentFIOCommand command)
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();

            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

            student.ChangeFIO(command.FirstName, command.MiddleName, command.LastName);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(student));

            var handler = new ChangeStudentFIOCommandHandler(_mockStudentRepository.Object);

            // Act
            await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            _mockStudentRepository.Verify(m => m.UpdateAsync(student), Times.Once());
        }

        [Theory, AutoData]
        public async Task ChangeFIOForStudentCommand_NotFoundStudent_ThrowObjectNotFoundException(ChangeStudentFIOCommand command)
        {
            // Arrange
            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(command.Id))
                .ReturnsAsync((Student)null);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Student>()));

            var handler = new ChangeStudentFIOCommandHandler(_mockStudentRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(action);
        }

        [Fact]
        public async Task ChangeFIOForStudentCommand_ChangeFIOWhenFirstNameEmpty_ThrowArgumentNullException()
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();

            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(student));

            var command = new ChangeStudentFIOCommand(student.Id, "", student.LastName, student.MiddleName);

            var handler = new ChangeStudentFIOCommandHandler(_mockStudentRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ChangeFIOForStudentCommand_ChangeFIOWhenLastNameEmpty_ThrowArgumentNullException()
        {
            // Arrange
            var student = StudentFactory.CorrectStudent();

            _mockStudentRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

            _mockStudentRepository
                .Setup(m => m.UpdateAsync(student));

            var command = new ChangeStudentFIOCommand(student.Id, student.FirstName, "", student.MiddleName);

            var handler = new ChangeStudentFIOCommandHandler(_mockStudentRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }
    }
}
