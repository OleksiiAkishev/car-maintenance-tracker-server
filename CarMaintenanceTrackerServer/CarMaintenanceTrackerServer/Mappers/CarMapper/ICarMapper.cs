using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.DTOs.Car.Response;

namespace CarMaintenanceTrackerServer.Mappers.CarMapper
{
    public interface ICarMapper
    {
        IEnumerable<GetAllCarsResponseDto> MapCarToGetAllCarsResponseDto(IEnumerable<Car> cars);
    }
}
