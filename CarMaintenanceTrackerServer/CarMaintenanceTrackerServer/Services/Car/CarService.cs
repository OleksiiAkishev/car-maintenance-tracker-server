using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.DTOs.Car.Response;

namespace CarMaintenanceTrackerServer.Services.Car
{
    public class CarService : ICarService
    {
        public CarService() { }

        public async Task<GetCarResponseDto> GetCar(int carId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetAllCarsResponseDto>> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public async Task<AddOrUpdateCarResponse> AddCar(AddOrUpdateCarRequestDto car)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCar(int carId)
        {
            throw new NotImplementedException();
        }

        public async Task<AddOrUpdateCarResponse> UpdateCar(int carId, AddOrUpdateCarRequestDto car)
        {
            throw new NotImplementedException();
        }
    }
}
