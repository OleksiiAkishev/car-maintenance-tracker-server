using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.DTOs.Car.Response;

namespace CarMaintenanceTrackerServer.Mappers.CarMapper
{
    public class CarMapper : ICarMapper
    {
        public IEnumerable<GetAllCarsResponseDto> MapCarToGetAllCarsResponseDto(IEnumerable<Car> cars)
        {
            throw new NotImplementedException();
        }
    }
}
