namespace CarMaintenanceTrackerServer.Result.Interfaces
{
    public interface IResult<T>
    {
        bool IsSuccess { get; }
        T? Value { get; }
        IResultError? Error { get; }
    }
}
