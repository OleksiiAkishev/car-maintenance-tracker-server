using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.DTOs.Car.Response;

namespace CarMaintenanceTrackerServer.Mappers.CarMapper
{
    public class CarMapper : ICarMapper
    {
        public Car MapAddCarRequestDtoToCar(AddCarRequestDto car)
        {
            throw new NotImplementedException();
        }

        public AddCarResponseDto MapCarToAddCarResponseDto(Car car)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetAllCarsResponseDto> MapCarToGetAllCarsResponseDto(IEnumerable<Car> cars)
        {
            throw new NotImplementedException();
        }

        public GetCarResponseDto MapCarToGetCarResponseDto(Car car)
        {
            throw new NotImplementedException();
        }

        public UpdateCarResponseDto MapCarToUpdateCarResponseDto(Car car)
        {
            throw new NotImplementedException();
        }

        public Car MapUpdateCarRequestDtoToCar(UpdateCarRequestDto car)
        {
            throw new NotImplementedException();
        }
    }
}
