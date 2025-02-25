namespace CarMaintenanceTrackerServer.Result.Interfaces
{
    public interface IServiceResult<out T>
    {
        bool IsSuccess { get; }
        T? Value { get; }
        IResultError? Error { get; }
    }
}
