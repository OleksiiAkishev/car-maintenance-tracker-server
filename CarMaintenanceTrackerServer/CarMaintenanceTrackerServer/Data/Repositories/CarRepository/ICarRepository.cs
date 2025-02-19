using CarEntity = CarMaintenanceTrackerServer.Data.Entities.Car;

namespace CarMaintenanceTrackerServer.Data.Repositories.Car
{
    public interface ICarRepository
    {
        Task<CarEntity> GetCar(int carId);
        Task<IEnumerable<CarEntity>> GetAllCars();
        Task<CarEntity> AddCar(CarEntity car);
        Task<CarEntity> UpdateCar(int carId, CarEntity car);
        Task<bool> DeleteCar(int carId);
    }
}
