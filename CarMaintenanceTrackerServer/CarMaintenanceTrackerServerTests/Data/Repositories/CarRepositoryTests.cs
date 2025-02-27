using CarMaintenanceTrackerServer.Data;
using Microsoft.EntityFrameworkCore;
using CarMaintenanceTrackerServer.Data.Repositories.CarRepository;
using CarMaintenanceTrackerServer.Data.Entities;

namespace CarMaintenanceTrackerServerTests.Data.Repositories
{
    public class CarRepositoryTests : IDisposable
    {
        private readonly CarRepository carRepository;
        private readonly ServerDbContext dbContext;

        public CarRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ServerDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            dbContext = new ServerDbContext(dbContextOptions);
            carRepository = new CarRepository(dbContext);
        }

        [Fact]
        public async Task GetCar_WhenCallWithExistingCarId_ShouldReturnTheCarFromDb()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var car = new Car()
            {
                Id = Guid.NewGuid(),
                Maker = "Toyota",
                Model = "Corolla",
                Year = 2010,
                UserId = userId,
                User = new User() 
                {
                    Id = userId,
                    Username = "Bob",
                },
            };
            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.carRepository.GetCar(car.Id);

            // Assert
            Assert.Equal(car, result);
        }

        [Fact]
        public async Task GetCar_WhenCallWithNonExistingCarId_ShouldReturnNull()
        {
            // Arrange
            var car = new Car()
            {
                Id = Guid.NewGuid(),
                Maker = "Toyota",
                Model = "Corolla",
                Year = 2010,
                User = new User()
                {
                    Username = "Bob",
                },
            };
            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.carRepository.GetCar(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCars_WhenCall_ReturnsAllCarsFromDb()
        {
            // Arrange
            var car1 = new Car()
            {
                Id = Guid.NewGuid(),
                Maker = "Toyota",
                Model = "Corolla",
                Year = 2010,
                User = new User()
                {
                    Username = "Bob",
                },
            };
            var car2 = new Car()
            {
                Id = Guid.NewGuid(),
                Maker = "Honda",
                Model = "Civic",
                Year = 2015,
                User = new User()
                {
                    Username = "Bob",
                },
            };
            await this.dbContext.Cars.AddAsync(car1);
            await this.dbContext.Cars.AddAsync(car2);
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.carRepository.GetAllCars();

            // Assert
            Assert.Equal([car1, car2], result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddCar_WhenCall_ShouldAddTheCarThenSaveChangesAndReturnTheSameCar()
        {
            // Arrange
            var car = new Car()
            {
                Id = Guid.NewGuid(),
                Maker = "Toyota",
                Model = "Corolla",
                Year = 2010,
                User = new User()
                {
                    Username = "Bob",
                },
            };

            // Act
            var result = await this.carRepository.AddCar(car);
            var expectedResult = await this.carRepository.GetCar(car.Id);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task UpdateCar_WhenCall_ShouldUpdateTheCarThenSaveChangesAndReturnTheSameCar()
        {
            // Arrange
            var car = new Car()
            {
                Id = Guid.NewGuid(),
                Maker = "Toyota",
                Model = "Corolla",
                Year = 2010,
                User = new User()
                {
                    Username = "Bob",
                },
            };
            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();

            // Act
            car.Maker = "Honda";
            car.Model = "Civic";
            var result = await this.carRepository.UpdateCar(car);
            var expectedResult = await this.carRepository.GetCar(car.Id);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task DeleteCar_WhenCall_ShouldSetIsDeletedToTrueThenSaveChangesAndReturnTrue()
        {
            // Arrange
            var car = new Car()
            {
                Id = Guid.NewGuid(),
                Maker = "Toyota",
                Model = "Corolla",
                Year = 2010,
                User = new User()
                {
                    Username = "Bob",
                },
            };
            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.carRepository.DeleteCar(car);
            var expectedResult = await this.carRepository.GetCar(car.Id);

            // Assert
            Assert.True(result);
            Assert.True(expectedResult?.IsDeleted);
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
