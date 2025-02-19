namespace CarMaintenanceTrackerServer.Result.Interfaces
{
    public interface IResultError
    {
        string Code { get; }
        string Message { get; }
        string? Detail { get; }
    }
}
