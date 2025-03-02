using CarMaintenanceTrackerServer.DTOs.Car.Request;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenanceTrackerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet("{carId}")]
        public IActionResult GetCar(int carId)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddCar(AddCarRequestDto car)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCar(AddCarRequestDto car)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCar(int carId)
        {
            return Ok();
        }
    }
}
