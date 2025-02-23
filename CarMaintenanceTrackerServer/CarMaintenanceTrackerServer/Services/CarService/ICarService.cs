using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.DTOs.Car.Response;

namespace CarMaintenanceTrackerServer.Services.Car
{
    public interface ICarService
    {
        Task<GetCarResponseDto> GetCar(int carId);
        Task<IEnumerable<GetAllCarsResponseDto>> GetAllCars();
        Task<AddOrUpdateCarResponse> AddCar(AddOrUpdateCarRequestDto car);
        Task<AddOrUpdateCarResponse> UpdateCar(int carId, AddOrUpdateCarRequestDto car);
        Task<bool> DeleteCar(int carId);
    }
}
