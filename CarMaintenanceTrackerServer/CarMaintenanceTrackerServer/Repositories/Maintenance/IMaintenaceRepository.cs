using CarMaintenanceTrackerServer.Entities;

namespace CarMaintenanceTrackerServer.Repositories.Maintenance
{
    public interface IMaintenaceRepository
    {
        Task<Reminder> LogMaintenance(MaintenanceLog maintenanceLog);
        Task<IEnumerable<Reminder>> GetMaintenanceHistory(int carId);
        Task<Reminder> SetReminder(Reminder reminder);
    }
}
