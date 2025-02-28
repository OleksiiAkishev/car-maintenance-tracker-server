using System.ComponentModel;

namespace CarMaintenanceTrackerServer.Result
{
    public enum CarErrorDetailsCodes
    {
        [Description("Get all cars error")]
        GET_ALL_CARS_ERROR,
        [Description("Get car error")]
        GET_CAR_ERROR,
    }
}
