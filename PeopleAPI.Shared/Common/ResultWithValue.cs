using PeopleAPI.Shared.Common;

namespace PeopleAPI.Shared.Common;

public class ResultWithValue<T> : Result
{
    public T? Value { get; }

    private ResultWithValue(bool isSuccess, T? value, string? errorMessage = null, string? successMessage = null)
        : base(isSuccess, errorMessage, successMessage)
    {
        Value = value;
    }

    public static ResultWithValue<T> Success(T value, string successMessage) => new ResultWithValue<T>(true, value, successMessage: successMessage);

    public static ResultWithValue<T> Failure(string errorMessage) => new ResultWithValue<T>(false, default, errorMessage: errorMessage);

    public override string ToString() =>
        IsSuccess ? $"Success: {SuccessMessage}" : $"Failure: {ErrorMessage}";
}
