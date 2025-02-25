using CarMaintenanceTrackerServer.Result.Interfaces;

namespace CarMaintenanceTrackerServer.Result
{
    public class SuccessResult<T>(T value) : IServiceResult<T> where T : notnull
    {
        public bool IsSuccess { get; } = true;

        public T Value { get; } = value;

        public IResultError? Error { get; } = null;
    }
}
