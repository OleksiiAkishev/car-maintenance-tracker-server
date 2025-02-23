namespace CarMaintenanceTrackerServer.Result.Interfaces
{
    public interface IResult
    {
        bool IsSuccess { get; }
        IResultError? Error { get; }
    }

    public interface ISuccessResult<out T> : IResult where T : notnull
    {
        T Value { get; }
    }

    public interface IFailureResult<out T> : IResult
    {
        T? Value { get; }
    }
}
