namespace Antix.Assertions;

public static class IsAnyExtensions
{
    public static Assertion<T> Null<T>(
        this Is @is,
        T? value
        ) => new()
        {
            Test = () => value is null,
            Negate = @is.Negate,
            FailMessage = "null"
        };
}
