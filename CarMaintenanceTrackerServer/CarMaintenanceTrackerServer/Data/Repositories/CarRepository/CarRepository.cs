using CarMaintenanceTrackerServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenanceTrackerServer.Data.Repositories.CarRepository
{
    public class CarRepository(ServerDbContext dbContext) : ICarRepository
    {
        private readonly ServerDbContext dbContext = dbContext;

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await this.dbContext.Cars.ToListAsync();
        }

        public async Task<Car?> GetCar(Guid carId)
        {
            return await this.dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        }

        public async Task<Car?> AddCar(Car car)
        {
            await this.dbContext.Cars.AddAsync(car);
            await this.dbContext.SaveChangesAsync();
            return car;
        }

        public async Task<Car?> UpdateCar(Car car)
        {
            this.dbContext.Cars.Update(car);
            await this.dbContext.SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteCar(Car car)
        {
            car.IsDeleted = true;
            this.dbContext.Cars.Update(car);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
