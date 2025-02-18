using CarMaintenanceTrackerServer.DTOs.Maintenance.Request;
using CarMaintenanceTrackerServer.DTOs.Maintenance.Response;

namespace CarMaintenanceTrackerServer.Services.Maintenance
{
    public class MaintenanceService : IMaintenanceService
    {
        public MaintenanceService()
        {
            
        }

        public Task<LogMaintenanceResponseDto> LogMaintenance(LogMaintenanceRequestDto maintenanceLog)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetLogMaintenanceResponseDto>> GetMaintenanceHistory(int carId)
        {
            throw new NotImplementedException();
        }

        public Task<ReminderResponseDto> SetReminder(ReminderRequestDto reminder)
        {
            throw new NotImplementedException();
        }
    }
}
