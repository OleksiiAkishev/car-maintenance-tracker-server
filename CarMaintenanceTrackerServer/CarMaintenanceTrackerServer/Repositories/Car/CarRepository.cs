using CarEntity = CarMaintenanceTrackerServer.Entities.Car;

namespace CarMaintenanceTrackerServer.Repositories.Car
{
    public class CarRepository : ICarRepository
    {
        public CarRepository()
        {
            
        }
        public Task<CarEntity> AddCar(CarEntity car)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCar(int carId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entities.Car>> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public Task<CarEntity> GetCar(int carId)
        {
            throw new NotImplementedException();
        }

        public Task<CarEntity> UpdateCar(int carId, CarEntity car)
        {
            throw new NotImplementedException();
        }
    }
}
