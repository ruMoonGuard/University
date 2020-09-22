using AutoFixture.Xunit2;
using Moq;
using System;
using System.Threading.Tasks;
using University.Application.Commands.ChangeGroupName;
using University.Application.Exceptions;
using University.Application.Interfaces.Repositories;
using University.Domain.Entities;
using Xunit;

namespace University.Appication.Test
{
    public class ChangeGroupNameCommandTest
    {
        private readonly Mock<IGroupRepository> _mockGroupRepository;

        public ChangeGroupNameCommandTest()
        {
            _mockGroupRepository = new Mock<IGroupRepository>();
        }

        [Theory, AutoData]
        public async Task UpdateGroupCommand_UserUpdatesGroup_Successfully(ChangeGroupNameCommand command, Group group)
        {
            // Arrange
            _mockGroupRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(group);

            _mockGroupRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Group>()));

            var handler = new ChangeGroupNameCommandHandler(_mockGroupRepository.Object);

            // Act
            await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            _mockGroupRepository.Verify(m => m.UpdateAsync(It.IsAny<Group>()), Times.Once());
        }

        [Theory, AutoData]
        public async Task UpdateGroupCommand_UserUpdatesGroupOnEmptyName_ThrowArgumentNullException(Group group)
        {
            // Arrange
            _mockGroupRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(group);

            _mockGroupRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Group>()));

            var command = new ChangeGroupNameCommand(group.Id, "");
            var handler = new ChangeGroupNameCommandHandler(_mockGroupRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Theory, AutoData]
        public async Task UpdateGroupCommand_GroupNotFoundForUpdate_ThrowObjectNotFoundException(ChangeGroupNameCommand command)
        {
            // Arrange
            _mockGroupRepository
                .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Group)null);

            _mockGroupRepository
                .Setup(m => m.UpdateAsync(It.IsAny<Group>()));

            var handler = new ChangeGroupNameCommandHandler(_mockGroupRepository.Object);

            // Act
            Func<Task> action = async () => await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(action);
        }
    }
}
