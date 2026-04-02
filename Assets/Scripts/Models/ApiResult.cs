namespace Assets.Scripts.Models
{
    public readonly struct ApiResult
    {
        public bool Ok { get; }
        public string Error { get; }

        public ProblemDetailError ErrorDetails { get; }

        private ApiResult(bool ok, string error, ProblemDetailError errorDetails = null)
        {
            Ok = ok;
            Error = error;
            ErrorDetails = errorDetails;
        }

        public static ApiResult Success() => new ApiResult(true, "");
        public static ApiResult Fail(string error) => new ApiResult(false, error ?? "Unknown error");
        public static ApiResult Fail(ProblemDetailError errorDetails) => new ApiResult(false, errorDetails?.Title ?? "Unknown error", errorDetails);
    }

    public readonly struct ApiResult<T>
    {
        public bool Ok { get; }
        public string Error { get; }
        public T Value { get; }

        public ProblemDetailError ErrorDetails { get; }

        private ApiResult(bool ok, string error, T value, ProblemDetailError errorDetails = null)
        {
            Ok = ok;
            Error = error;
            Value = value;
            ErrorDetails = errorDetails;
        }

        public static ApiResult<T> Success(T value) => new ApiResult<T>(true, "", value);
        public static ApiResult<T> Fail(string error) => new ApiResult<T>(false, error ?? "Unknown error", default);
        public static ApiResult<T> Fail(ProblemDetailError errorDetails) => new ApiResult<T>(false, errorDetails?.Title ?? "Unknown error", default, errorDetails);
    }
}
