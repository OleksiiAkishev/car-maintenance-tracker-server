using CarMaintenanceTrackerServer.Data.Entities;

namespace CarMaintenanceTrackerServer.Data.Repositories.CarRepository
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car?> GetCar(Guid carId);
        Task<Car> AddCar(Car car);
        Task<Car> UpdateCar(Car car);
        Task<bool> DeleteCar(Car car);
    }
}
