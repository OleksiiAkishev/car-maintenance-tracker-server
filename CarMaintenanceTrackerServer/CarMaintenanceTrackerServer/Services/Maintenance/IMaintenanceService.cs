using CarMaintenanceTrackerServer.DTOs.Maintenance.Request;
using CarMaintenanceTrackerServer.DTOs.Maintenance.Response;

namespace CarMaintenanceTrackerServer.Services.Maintenance
{
    public interface IMaintenanceService
    {
        Task<LogMaintenanceResponseDto> LogMaintenance(LogMaintenanceRequestDto maintenanceLog);
        Task<IEnumerable<GetLogMaintenanceResponseDto>> GetMaintenanceHistory(int carId);
        Task<ReminderResponseDto> SetReminder(ReminderRequestDto reminder);
    }
}
