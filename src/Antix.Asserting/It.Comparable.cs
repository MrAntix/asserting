using System;
using System.Collections.Generic;

namespace Antix.Asserting;

public static partial class Is
{
    public static Func<ItContext<T>, ItContext<T>> EqualTo<T>(
        T? comparison
        )
        where T : IComparable<T>
        => context => context.And(
            value => new()
            {
                TestSuccess = () => Comparer<T>.Default.Compare(value, comparison) == 0,
                FailMessage = $"greater-than({comparison})"
            }
        );

    public static Func<ItContext<T>, ItContext<T>> GreaterThan<T>(
        T? comparison
        )
        where T : IComparable<T>
        => context => context.And(
            value => new()
            {
                TestSuccess = () => Comparer<T>.Default.Compare(value, comparison) > 0,
                FailMessage = $"greater-than({comparison})"
            }
        );

    public static Func<ItContext<T>, ItContext<T>> LessThan<T>(
        T? comparison
        )
        where T : IComparable<T>
        => context => context.And(
            value => new()
            {
                TestSuccess = () => Comparer<T>.Default.Compare(value, comparison) < 0,
                FailMessage = $"less-than({comparison})"
            }
        );
}