using System;
using System.Collections.Generic;

namespace Antix.Asserting;

public static class IsComparable
{
    public static bool GreaterOrEqualTo<T>(
        this IValidate<T> context,
        T comparison
        )
        where T : IComparable<T>
        => context.AssertNotNull(
            value => Comparer<T>.Default.Compare(value, comparison) >= 0,
            $"greater-or-equal-to({comparison})"
            );

    public static bool GreaterThan<T>(
        this IValidate<T> context,
        T comparison
        )
        where T : IComparable<T>
        => context.AssertNotNull(
            value => Comparer<T>.Default.Compare(value, comparison) > 0,
            $"greater-than({comparison})"
            );

    public static bool LessOrEqualTo<T>(
        this IValidate<T> context,
        T comparison
        )
        where T : IComparable<T>
        => context.AssertNotNull(
            value => Comparer<T>.Default.Compare(value, comparison) <= 0,
            $"less-or-equal-to({comparison})"
            );

    public static bool LessThan<T>(
        this IValidate<T> context,
        T comparison
        )
        where T : IComparable<T>
        => context.AssertNotNull(
            value => Comparer<T>.Default.Compare(value, comparison) < 0,
            $"less-than({comparison})"
            );
}