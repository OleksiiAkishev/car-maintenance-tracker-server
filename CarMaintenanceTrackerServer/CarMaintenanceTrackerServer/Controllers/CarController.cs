using CarMaintenanceTrackerServer.DTOs.Car.Request;
using CarMaintenanceTrackerServer.Services.CarService;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenanceTrackerServer.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController(ICarService carService, ILogger logger) : ControllerBase
    {
        private readonly ICarService carService = carService;
        private readonly ILogger logger = logger;

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCar(Guid carId)
        {
            try 
            {
                var result = await this.carService.GetCar(carId);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("Car not found.");
                    return NotFound(result);
                }
                this.logger.LogInformation("Car found successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while getting the car by \"Id=\"{carId}.";
                this.logger.LogError(ex, "An error occurred while getting the car by \"Id=\"{CarId}", carId);
                return StatusCode(500, message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var result = await this.carService.GetAllCars();
                if (!result.IsSuccess)
                {
                    this.logger.LogError("Cars not found.");
                    return NotFound(result);
                }
                this.logger.LogInformation("Cars found successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while getting the cars.";
                this.logger.LogError(ex, "An error occurred while getting the car by");
                return StatusCode(500, message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarRequestDto car)
        {
            try
            {
                var result = await this.carService.AddCar(car);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("Car not added.");
                    return StatusCode(500, result);
                }
                this.logger.LogInformation("Car added successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while adding the car. \"Maker=\"{car.Maker}, \"Model=\"{car.Model}, \"Username=\"{car.User.Username}.";
                this.logger.LogError(ex, "An error occurred while adding the car.");
                return StatusCode(500, message);
            }
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCar(Guid carId, UpdateCarRequestDto car)
        {
            try
            {
                var result = await this.carService.UpdateCar(carId, car);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("Car not updated.");
                    return StatusCode(500, result);
                }
                this.logger.LogInformation("Car updated successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while updating the car by \"Id=\"{carId}.";
                this.logger.LogError(ex, "An error occurred while updating the car by \"Id=\"{CarId}.", carId);
                return StatusCode(500, message);
            }
        }

        [HttpDelete("{carId}")]
        public async Task<IActionResult> DeleteCar(Guid carId)
        {
            try
            {
                var result = await this.carService.DeleteCar(carId);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("Car not deleted.");
                    return StatusCode(500, result);
                }
                this.logger.LogInformation("Car deleted successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while deleting the car by \"Id=\"{carId}.";
                this.logger.LogError(ex, "An error occurred while deleting the car by \"Id=\"{CarId}.", carId);
                return StatusCode(500, message);
            }
        }
    }
}
