using System.ComponentModel;

namespace CarMaintenanceTrackerServer.Result
{
    public enum CarErrorDetailsCodes
    {
        [Description("Get all cars error")]
        GET_ALL_CARS_ERROR,
        [Description("Get car error")]
        GET_CAR_ERROR,
        [Description("Add car error")]
        ADD_CAR_ERROR,
        [Description("Update car error")]
        UPDATE_CAR_ERROR,
        [Description("Delete car error")]
        DELETE_CAR_ERROR
    }
}
