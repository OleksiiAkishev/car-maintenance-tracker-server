using CarMaintenanceTrackerServer.Result.Interfaces;

namespace CarMaintenanceTrackerServer.Result
{
    public class FailureResult<T>(IResultError error) : IResult<T>
    {
        public bool IsSuccess { get; } = false;

        public T? Value { get; } = default;

        public IResultError? Error { get; } = error;
    }
}
