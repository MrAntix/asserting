using System;

namespace Antix.Asserting;

public static class IsAny
{
    public static bool Null<T>(
        this IValidate<T> context
        ) => context.Assert(Is<T>.Null);

    public static bool EqualTo<T>(
        this IValidate<T> context,
        T comparison
        ) where T : IEquatable<T>
        => context.Assert(Is<T>.EqualTo(comparison));
}
