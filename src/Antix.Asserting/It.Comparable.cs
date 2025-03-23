using System;
using System.Collections.Generic;

namespace Antix.Asserting;

public static partial class Is
{
    public static Func<T, NotNullAssertion> EqualTo<T>(
        T? comparison
        )
        where T : IComparable<T>
        => value => new()
        {
            Success = Comparer<T>.Default.Compare(value, comparison) == 0,
            FailMessage = $"equal-to({comparison})"
        };

    public static Func<T, NotNullAssertion> GreaterThan<T>(
        T? comparison
        )
        where T : IComparable<T>
        => value => new()
        {
            Success = Comparer<T>.Default.Compare(value, comparison) > 0,
            FailMessage = $"greater-than({comparison})"
        };

    public static Func<T, NotNullAssertion> LessThan<T>(
        T? comparison
        )
        where T : IComparable<T>
        => value => new()
        {
            Success = Comparer<T>.Default.Compare(value, comparison) < 0,
            FailMessage = $"less-than({comparison})"
        };
}