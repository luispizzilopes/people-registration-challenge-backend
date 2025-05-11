namespace PeopleAPI.Shared.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public string? SuccessMessage { get; }

    protected Result(bool isSuccess, string? errorMessage = null, string? successMessage = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        SuccessMessage = successMessage;
    }

    public static Result Success(string successMessage) => new(true, successMessage: successMessage);

    public static Result Failure(string errorMessage) => new(false, errorMessage: errorMessage);

    public override string ToString() =>
        IsSuccess ? $"Success: {SuccessMessage}" : $"Failure: {ErrorMessage}";
}
