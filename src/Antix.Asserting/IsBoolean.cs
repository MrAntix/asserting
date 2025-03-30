namespace Antix.Asserting;

public static class IsBoolean
{
    public static bool True(
        this IValidate<bool> context
        ) => context.Assert(Is.True);

    public static bool False(
        this IValidate<bool> context
        ) => context.Assert(Is.False);
}
