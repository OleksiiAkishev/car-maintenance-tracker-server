using CarMaintenanceTrackerServer.Result.Interfaces;

namespace CarMaintenanceTrackerServer.Result
{
    public static class ResultFactory
    {
        public static ISuccessResult<T> CreateSuccessResult<T>(T value) where T : notnull
        {
            return new SuccessResult<T>(value);
        }

        public static IFailureResult<T> CreateFailureResult<T>(IResultError error)
        {
            return new FailureResult<T>(new ErrorDetails(error.Code, error.Message, error.Detail));
        }
    }
}
