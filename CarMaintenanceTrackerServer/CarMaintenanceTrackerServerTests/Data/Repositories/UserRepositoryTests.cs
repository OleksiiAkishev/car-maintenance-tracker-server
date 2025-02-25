using CarMaintenanceTrackerServer.Data;
using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.Data.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CarMaintenanceTrackerServerTests.Data.Repositories
{ 
    public class UserRepositoryTests
    {
        private readonly UserRepository userRepository;
        private readonly Mock<CarMaintenanceTrackerDbContext> carMaintenanceTrackerDbContextMock;
        private readonly Mock<DbSet<User>> usersDbSetMock;

        public UserRepositoryTests() 
        {
            carMaintenanceTrackerDbContextMock = new Mock<CarMaintenanceTrackerDbContext>();
            usersDbSetMock = new Mock<DbSet<User>>();
            userRepository = new UserRepository(carMaintenanceTrackerDbContextMock.Object);
        }

        [Fact]
        public async Task RegisterUser_WhenCall_ShouldAddTheUserThenSaveChangesAndReturnTheSameUser() 
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
            };
            carMaintenanceTrackerDbContextMock.Setup(m => m.Users).Returns(usersDbSetMock.Object);

            // Act
            var result = await userRepository.RegisterUser(user);

            // Assert
            usersDbSetMock.Verify(m => m.Add(It.IsAny<User>()), Times.Once);
            carMaintenanceTrackerDbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(user, result);
        }
    }
}
