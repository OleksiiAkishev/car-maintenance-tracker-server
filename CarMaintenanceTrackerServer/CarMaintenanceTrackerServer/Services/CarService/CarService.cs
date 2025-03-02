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
            try
            {
                var car = await carRepository.GetCar(carId);
                if (car == null) 
                {
                    this.logger.LogError("Car with \"Id=\"{CarId} was not found.", carId);
                    return ResultFactory.CreateFailureResult<GetCarResponseDto>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.GET_CAR_ERROR.GetDisplayName(), $"Car was not found."));
                }
                return ResultFactory.CreateSuccessResult(this.carMapper.MapCarToGetCarResponseDto(car));
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while getting car by \"Id=\"{CarId}.", carId);
                return ResultFactory.CreateFailureResult<GetCarResponseDto>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.GET_CAR_ERROR.GetDisplayName(), ex.Message));
            }
        }

        public async Task<IServiceResult<AddCarResponseDto>> AddCar(AddCarRequestDto car)
        {
            try
            {
                if (car == null) 
                {
                    this.logger.LogError("Requested add car is null.");
                    return ResultFactory.CreateFailureResult<AddCarResponseDto>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.ADD_CAR_ERROR.GetDisplayName(), "Provided car is null."));
                }
                var result = await this.carRepository.AddCar(this.carMapper.MapAddCarRequestDtoToCar(car));
                return ResultFactory.CreateSuccessResult(this.carMapper.MapCarToAddCarResponseDto(result));
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while adding a car. \"Maker=\"{Maker}, \"Model=\"{Model}, \"Username=\"{UserName} ", car.Maker, car.Model, car.User.Username);
                return ResultFactory.CreateFailureResult<AddCarResponseDto>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.ADD_CAR_ERROR.GetDisplayName(), ex.Message));
            }
        }

        public async Task<IServiceResult<UpdateCarResponseDto>> UpdateCar(Guid carId, UpdateCarRequestDto car)
        {
            try
            {
                var carEntity = await this.carRepository.GetCar(carId);
                if (carEntity == null)
                {
                    this.logger.LogError("Car with \"Id=\"{CarId} was not found.", carId);
                    return ResultFactory.CreateFailureResult<UpdateCarResponseDto>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.UPDATE_CAR_ERROR.GetDisplayName(), $"Car was not found."));
                }
                if (!string.IsNullOrEmpty(car.Maker) && car.Maker != carEntity.Maker)
                {
                    carEntity.Maker = car.Maker;
                }
                if (!string.IsNullOrEmpty(car.Model) && car.Model != carEntity.Model)
                {
                    carEntity.Model = car.Model;
                }
                if (car.Year != carEntity.Year)
                {
                    carEntity.Year = car.Year;
                }
                if (!string.IsNullOrEmpty(car.LicensePlate) && car.LicensePlate != carEntity.LicensePlate)
                {
                    carEntity.LicensePlate = car.LicensePlate;
                }
                var updatedCar = await this.carRepository.UpdateCar(carEntity);
                return ResultFactory.CreateSuccessResult(this.carMapper.MapCarToUpdateCarResponseDto(updatedCar));
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while updating a car. \"Id=\"{CarId}", carId);
                return ResultFactory.CreateFailureResult<UpdateCarResponseDto>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.UPDATE_CAR_ERROR.GetDisplayName(), ex.Message));
            }
        }

        public async Task<IServiceResult<bool>> DeleteCar(Guid carId)
        {
            try
            {
                var carEntity = await this.carRepository.GetCar(carId);
                if (carEntity == null)
                {
                    this.logger.LogError("Car with \"Id=\"{CarId} was not found.", carId);
                    return ResultFactory.CreateFailureResult<bool>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.DELETE_CAR_ERROR.GetDisplayName(), $"Car was not found."));
                }
                var result = await this.carRepository.DeleteCar(carEntity);
                return ResultFactory.CreateSuccessResult(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while deleting a car. \"Id=\"{CarId}", carId);
                return ResultFactory.CreateFailureResult<bool>(ResultFactory.CreateErrorDetails(CarErrorDetailsCodes.DELETE_CAR_ERROR.GetDisplayName(), ex.Message));
            }
        }
    }
}
