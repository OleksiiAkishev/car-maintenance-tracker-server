using CarMaintenanceTrackerServer.Data.Repositories.CarRepository;
using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.DTOs.Car.Response;
using CarMaintenanceTrackerServer.Mappers.CarMapper;
using CarMaintenanceTrackerServer.Result;
using CarMaintenanceTrackerServer.Result.Interfaces;
using Microsoft.OpenApi.Extensions;

namespace CarMaintenanceTrackerServer.Services.CarService
{
    public class CarService(ICarRepository carRepository, ICarMapper carMapper, ILogger logger) : ICarService
    {
        private readonly ICarRepository carRepository = carRepository;
        private readonly ICarMapper carMapper = carMapper;
        private readonly ILogger logger = logger;

        public async Task<IServiceResult<IEnumerable<GetAllCarsResponseDto>>> GetAllCars()
        {
            try
            {
                var cars = await carRepository.GetAllCars();
                var successResult = ResultFactory.CreateSuccessResult(this.carMapper.MapCarToGetAllCarsResponseDto(cars));
                return successResult;
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while getting all cars.");
                return ResultFactory.CreateFailureResult<IEnumerable<GetAllCarsResponseDto>>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.GET_ALL_CARS_ERROR.GetDisplayName(), ex.Message));
            }
        }

        public async Task<IServiceResult<GetCarResponseDto>> GetCar(Guid carId)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<AddOrUpdateCarResponse>> AddCar(AddOrUpdateCarRequestDto car)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<AddOrUpdateCarResponse>> UpdateCar(Guid carId, AddOrUpdateCarRequestDto car)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<bool>> DeleteCar(Guid carId)
        {
            throw new NotImplementedException();
        }
    }
}
