using AutoFixture.Xunit2;
using Moq;
using System;
using System.Threading.Tasks;
using University.Application.Commands.CreateGroup;
using University.Domain.Entities;
using University.Domain.Interfaces;
using Xunit;

namespace University.Appication.Test
{
    public class CreateGroupCommandTest
    {
        private readonly Mock<IGroupRepository> _mockGroupRepository;

        public CreateGroupCommandTest()
        {
            _mockGroupRepository = new Mock<IGroupRepository>();
        }

        [Theory, AutoData]
        public async Task CreateGroupCommand_CreatesGroup_Successfully(CreateGroupCommand command)
        {
            // Arrange
            _mockGroupRepository
                .Setup(m => m.AddAsync(It.IsAny<Group>()))
                .ReturnsAsync(It.IsAny<Guid>());

            var handler = new CreateGroupCommandHandler(_mockGroupRepository.Object);

            // Act
            await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            _mockGroupRepository.Verify(m => m.AddAsync(It.IsAny<Group>()), Times.Once());
        }

        [Fact]
        public async Task CreateGroupCommand_CreatesGroupWithEmptyName_ThrowArgumentException()
        {
            // Arrange
            _mockGroupRepository
                .Setup(m => m.AddAsync(It.IsAny<Group>()))
                .ReturnsAsync(It.IsAny<Guid>());

            var command = new CreateGroupCommand("");

            var handler = new CreateGroupCommandHandler(_mockGroupRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }
    }
}
