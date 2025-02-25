using CarMaintenanceTrackerServer.Data;
using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.Data.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenanceTrackerServerTests.Data.Repositories
{ 
    public class UserRepositoryTests : IDisposable
    {
        private readonly UserRepository userRepository;
        private readonly CarMaintenanceTrackerDbContext carMaintenanceTrackerDbContext;

        public UserRepositoryTests() 
        {
            var dbContextOptions = new DbContextOptionsBuilder<CarMaintenanceTrackerDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            carMaintenanceTrackerDbContext = new CarMaintenanceTrackerDbContext(dbContextOptions);
            userRepository = new UserRepository(carMaintenanceTrackerDbContext);
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

            // Act
            var result = await this.userRepository.RegisterUser(user);
            var expectedResult = await this.userRepository.GetUserByUsername(user.Username);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task LoginUser_WhenCallWithExistingUser_ShouldReturnTheUserFromDbByUserId()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
            };
            await this.carMaintenanceTrackerDbContext.Users.AddAsync(user);
            await this.carMaintenanceTrackerDbContext.SaveChangesAsync();

            // Act
            var result = await this.userRepository.LoginUser(user.Id);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LoginUser_WhenCallWithNonExistingUser_ShouldReturnNull()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
            };

            // Act
            var result = await this.userRepository.LoginUser(user.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserById_WhenCallWithExistingUser_ShouldReturnTheUserFromDbByUserId()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
                Email = "User1@domain.com",
            };
            await this.carMaintenanceTrackerDbContext.Users.AddAsync(user);
            await this.carMaintenanceTrackerDbContext.SaveChangesAsync();

            // Act
            var result = await this.userRepository.GetUserById(user.Id);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserById_WhenCallWithNonExistingUser_ShouldReturnNull()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
            };

            // Act
            var result = await this.userRepository.GetUserById(user.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByUsername_WhenCallWithExistingUser_ShouldReturnTheUserFromDbByUserId()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
            };
            await this.carMaintenanceTrackerDbContext.Users.AddAsync(user);
            await this.carMaintenanceTrackerDbContext.SaveChangesAsync();

            // Act
            var result = await this.userRepository.GetUserByUsername(user.Username);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserByUsername_WhenCallWithNonExistingUser_ShouldReturnNull()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
            };

            // Act
            var result = await this.userRepository.GetUserByUsername(user.Username);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_WhenCallWithExistingUser_ShouldUpdateUser()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
                Email = "user1@domain.com"
            };
            await this.carMaintenanceTrackerDbContext.Users.AddAsync(user);
            await this.carMaintenanceTrackerDbContext.SaveChangesAsync();
            user.Email = "user11@domain.com";

            // Act
            await this.userRepository.UpdateUser(user);

            // Assert
            var userFromDb = await carMaintenanceTrackerDbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            Assert.NotNull(userFromDb);
            Assert.Equal("user11@domain.com", userFromDb.Email);
            Assert.Equal(user.Id, userFromDb.Id);
        }

        [Fact]
        public async Task DeleteUser_WhenCallWithExistingUser_ShouldDeleteUser()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
                Email = "user1@domain.com"
            };
            await this.carMaintenanceTrackerDbContext.Users.AddAsync(user);
            await this.carMaintenanceTrackerDbContext.SaveChangesAsync();

            // Act
            var result = await this.userRepository.DeleteUser(user.Id);

            // Assert
            Assert.True(result);
            var userFromDb = await carMaintenanceTrackerDbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            Assert.Null(userFromDb);
        }

        [Fact]
        public async Task DeleteUser_WhenCallWithNonExistingUser_ShouldReturnFalseAndNoUsersToBeDeletedFromDb()
        {
            // Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "User1",
                Email = "user1@domain.com"
            };
            await this.carMaintenanceTrackerDbContext.Users.AddAsync(user);
            await this.carMaintenanceTrackerDbContext.SaveChangesAsync();
            var initialUserCount = await this.carMaintenanceTrackerDbContext.Users.CountAsync();

            // Act
            var result = await this.userRepository.DeleteUser(Guid.NewGuid());
            var finalUserCount = await this.carMaintenanceTrackerDbContext.Users.CountAsync();

            // Assert
            Assert.False(result);
            Assert.Equal(initialUserCount, finalUserCount);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.carMaintenanceTrackerDbContext.Database.EnsureDeleted();
        }
    }
}
