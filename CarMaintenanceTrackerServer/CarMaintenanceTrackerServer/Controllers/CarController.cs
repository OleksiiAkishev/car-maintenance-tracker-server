using CarMaintenanceTrackerServer.DTOs.Car;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenanceTrackerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllCars()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddCar(CarRequestDto car)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCar(CarRequestDto car)
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
