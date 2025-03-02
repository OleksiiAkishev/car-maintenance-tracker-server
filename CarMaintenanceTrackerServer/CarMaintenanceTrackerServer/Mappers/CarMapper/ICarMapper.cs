using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.DTOs.Car.Response;

namespace CarMaintenanceTrackerServer.Mappers.CarMapper
{
    public interface ICarMapper
    {
        IEnumerable<GetAllCarsResponseDto> MapCarToGetAllCarsResponseDto(IEnumerable<Car> cars);
        GetCarResponseDto MapCarToGetCarResponseDto (Car car);
        AddCarResponseDto MapCarToAddCarResponseDto(Car car);
        Car MapAddCarRequestDtoToCar(AddCarRequestDto car);
        Car MapUpdateCarRequestDtoToCar(UpdateCarRequestDto car);
        UpdateCarResponseDto MapCarToUpdateCarResponseDto(Car car);
    }
}
