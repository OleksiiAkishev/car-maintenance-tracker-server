using CarMaintenanceTrackerServer.Entities;

namespace CarMaintenanceTrackerServer.Repositories.Maintenance
{
    public class MaintenaceRepository : IMaintenaceRepository
    {
        public MaintenaceRepository()
        {
            
        }

        public Task<IEnumerable<Reminder>> GetMaintenanceHistory(int carId)
        {
            throw new NotImplementedException();
        }

        public Task<Reminder> LogMaintenance(MaintenanceLog maintenanceLog)
        {
            throw new NotImplementedException();
        }

        public Task<Reminder> SetReminder(Reminder reminder)
        {
            throw new NotImplementedException();
        }
    }
}
