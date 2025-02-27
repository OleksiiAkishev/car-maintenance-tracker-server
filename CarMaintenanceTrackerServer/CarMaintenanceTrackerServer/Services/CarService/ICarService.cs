using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.DTOs.Car.Response;
using CarMaintenanceTrackerServer.Result.Interfaces;

namespace CarMaintenanceTrackerServer.Services.CarService
{
    public interface ICarService
    {
        Task<IServiceResult<GetCarResponseDto>> GetCar(Guid carId);
        Task<IServiceResult<IEnumerable<GetAllCarsResponseDto>>> GetAllCars();
        Task<IServiceResult<AddOrUpdateCarResponse>> AddCar(AddOrUpdateCarRequestDto car);
        Task<IServiceResult<AddOrUpdateCarResponse>> UpdateCar(Guid carId, AddOrUpdateCarRequestDto car);
        Task<IServiceResult<bool>> DeleteCar(Guid carId);
    }
}
