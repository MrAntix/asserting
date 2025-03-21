namespace Antix.Asserting;

public static partial class Is
{
    public static ItContext<T> Null<T>(
        ItContext<T> context
        ) => context.And(
            value => new()
            {
                TestSuccess = () => value is null,
                FailMessage = "null"
            });
}
