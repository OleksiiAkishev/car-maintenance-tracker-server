using CarMaintenanceTrackerServer.DTOs.Maintenance;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenanceTrackerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        [HttpPost("log")]
        public IActionResult LogMaintenance(MaintenanceLogRequestDto maintenanceLog)
        {
            return Ok();
        }

        [HttpGet("{carId}")]
        public IActionResult GetMaintenanceHistory(int carId)
        {
            return Ok();
        }

        [HttpPost("reminder")]
        public IActionResult SetReminder(ReminderRequestDto reminder)
        {
            return Ok();
        }
    }
}
