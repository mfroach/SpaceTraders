namespace SpaceTradersLib.Models;

public class Result<T> where T : class {
    public bool IsSuccess { get; }
    public T? Data { get; }
    public ApiError? Error { get; }

    private Result(bool isSuccess, T? data, ApiError? error) {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    public static Result<T> Success(T data) => new(true, data, null);
    public static Result<T> Failure(ApiError apiError) => new(false, null, apiError);
}