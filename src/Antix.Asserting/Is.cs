using System;
using System.Collections.Generic;
using System.Linq;

namespace Antix.Asserting;

public static class Is
{
    public static readonly NotNullAssertion<bool> True
        = new(value => value == true, "true");
    public static readonly NotNullAssertion<bool> False
        = new(value => value == false, "false");
}

public sealed class Is<T>
{
    public static readonly MaybeNullAssertion<T> Null = new(
        value => value is null,
        "null"
        );

    public static MaybeNullAssertion<T> EqualTo(
        T? comparison
        ) => new(
            value => EqualityComparer<T>.Default.Equals(value, comparison),
            $"equal-to({comparison})"
            );

    public static NotNullAssertion<IEnumerable<T?>> Length(
        int exactLength
        ) => new(
            value => value.Count() == exactLength,
            $"length({exactLength})"
            );

    public static NotNullAssertion<IEnumerable<T?>> LengthGreaterThan(
        int exactLength
        ) => new(
            value => value.Count() > exactLength,
            $"length-greater-than({exactLength})"
            );

    public static NotNullAssertion<IEnumerable<T?>> LengthGreaterOrEqualTo(
        int exactLength
        ) => new(
            value => value.Count() >= exactLength,
            $"length-greater-or-equal-to({exactLength})"
            );

    public static NotNullAssertion<IEnumerable<T?>> LengthLessThan(
        int exactLength
        ) => new(
            value => value.Count() < exactLength,
            $"length-less-than({exactLength})"
            );

    public static NotNullAssertion<IEnumerable<T?>> LengthLessOrEqualTo(
        int exactLength
        ) => new(
            value => value.Count() <= exactLength,
            $"length-less-or-equal-to({exactLength})"
            );
}
