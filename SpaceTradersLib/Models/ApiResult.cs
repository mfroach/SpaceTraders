namespace SpaceTradersLib.Models;

public class ApiResult : IDisposable {
    public ApiResult(bool isSuccess, Stream data) {
        IsSuccess = isSuccess;
        Data = data;
    }

    public bool IsSuccess { get; }
    public Stream Data { get; }

    public void Dispose() {
        Data.Dispose();
    }
}