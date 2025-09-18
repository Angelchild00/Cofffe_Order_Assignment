namespace CoffeeOrder.Validation;

public sealed class ValidationResult
{
    public bool IsValid { get; }
    public IReadOnlyList<string> Errors { get; }

    public ValidationResult(bool isValid, IEnumerable<string> errors)
    {
        IsValid = isValid;
        Errors = errors?.ToList() ?? new List<string>();
    }

    public static ValidationResult Ok() => new ValidationResult(true, Array.Empty<string>());
    public static ValidationResult Fail(params string[] errors) => new ValidationResult(false, errors ?? Array.Empty<string>());
}