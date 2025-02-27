using CarMaintenanceTrackerServer.Data;
using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.Data.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenanceTrackerServerTests.Data.Repositories
{ 
    public class UserRepositoryTests : IDisposable
    {
        private readonly UserRepository userRepository;
        private readonly ServerDbContext dbContext;

        public UserRepositoryTests() 
        {
            var dbContextOptions = new DbContextOptionsBuilder<ServerDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            dbContext = new ServerDbContext(dbContextOptions);
            userRepository = new UserRepository(dbContext);
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
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

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
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

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
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

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
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();
            user.Email = "user11@domain.com";

            // Act
            await this.userRepository.UpdateUser(user);

            // Assert
            var userFromDb = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
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
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.userRepository.DeleteUser(user);

            // Assert
            Assert.True(result);
            var userFromDb = await dbContext.Users.FirstAsync(u => u.Id == user.Id);
            Assert.True(userFromDb.IsDeleted);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.dbContext.Database.EnsureDeleted();
        }
    }
}
