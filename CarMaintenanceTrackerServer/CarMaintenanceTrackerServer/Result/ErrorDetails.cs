using CarMaintenanceTrackerServer.Result.Interfaces;

namespace CarMaintenanceTrackerServer.Result
{
    public class ErrorDetails(string errorCode, string errorMessage, string? detail = null) : IResultError
    {
        public string Code { get; } = errorCode;

        public string Message { get; } = errorMessage;

        public string? Detail { get; } = detail;
    }
}
