namespace Antix.Asserting;

public static partial class Is
{
    public static MaybeNullAssertion Null<T>(
        T value
        ) => new()
        {
            Success = value is null,
            FailMessage = "null"
        };

    public static MaybeNullAssertion NotNull<T>(
        T value
        ) => new()
        {
            Success = value is not null,
            FailMessage = "not-null",
            FailNotMessage = "null"
        };
}
