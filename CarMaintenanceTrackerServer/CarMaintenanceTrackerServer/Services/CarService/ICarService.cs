using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.DTOs.Car.Response;
using CarMaintenanceTrackerServer.Result.Interfaces;

namespace CarMaintenanceTrackerServer.Services.CarService
{
    public interface ICarService
    {
        Task<IServiceResult<GetCarResponseDto>> GetCar(Guid carId);
        Task<IServiceResult<IEnumerable<GetAllCarsResponseDto>>> GetAllCars();
        Task<IServiceResult<AddCarResponseDto>> AddCar(AddCarRequestDto car);
        Task<IServiceResult<UpdateCarResponseDto>> UpdateCar(Guid carId, UpdateCarRequestDto car);
        Task<IServiceResult<bool>> DeleteCar(Guid carId);
    }
}
