using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Antix.Asserting;

public static class IsEnumerable
{
    public static bool Length<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int length
        ) => context.Assert(Is<TItem?>.Length(length));

    public static bool LengthGreaterThan<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int length
        ) => context.Assert(Is<TItem?>.LengthGreaterThan(length));

    public static bool LengthGreaterOrEqualTo<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int length
        ) => context.Assert(Is<TItem?>.LengthGreaterOrEqualTo(length));

    public static bool LengthLessThan<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int length
        ) => context.Assert(Is<TItem?>.LengthLessThan(length));

    public static bool LengthLessOrEqualTo<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int length
        ) => context.Assert(Is<TItem?>.LengthLessOrEqualTo(length));

    public static bool CountOf<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int exactCount,
        TItem? item,
        [CallerArgumentExpression(nameof(item))] string? expression = null
        ) => Count(context,
            exactCount, exactCount,
            Is<TItem>.EqualTo(item),
            $"count-of({exactCount},{expression})"
            );

    public static bool CountOfMin<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int min,
        TItem? item,
        [CallerArgumentExpression(nameof(item))] string? expression = null
        ) => Count(context,
            min, null,
            Is<TItem>.EqualTo(item),
            $"count-of-min({min},{expression})"
            );

    public static bool CountOfMax<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int max,
        TItem? item,
        [CallerArgumentExpression(nameof(item))] string? expression = null
        ) => Count(context,
            null, max,
            Is<TItem>.EqualTo(item),
            $"count-of-max({max},{expression})"
            );

    public static bool CountOfMinMax<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int min, int max,
        TItem? item,
        [CallerArgumentExpression(nameof(item))] string? expression = null
        ) => Count(context,
            min, max,
            Is<TItem>.EqualTo(item),
            $"count-of-min-max({min},{max},{expression})"
            );

    public static bool CountOf<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int exactCount,
        IAssertion<TItem> assertion
        ) => Count(context,
            exactCount, exactCount,
            assertion,
            $"count-of({exactCount},{assertion.Error})"
            );

    public static bool CountOfMin<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int min,
        IAssertion<TItem> assertion
        ) => Count(context,
            min, null,
            assertion,
            $"count-of-min({min},{assertion.Error})"
            );

    public static bool CountOfMax<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int max,
        IAssertion<TItem> assertion
        ) => Count(context,
            null, max,
            assertion,
            $"count-of-max({max},{assertion.Error})"
            );

    public static bool CountOfMinMax<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int min, int max,
        IAssertion<TItem> assertion
        ) => Count(context,
            min, max,
            assertion,
            $"count-of-min-max({min},{max},{assertion.Error})"
            );

    static bool Count<TItem>(
        this IValidate<IEnumerable<TItem?>> context,
        int? min, int? max,
        IAssertion<TItem> assertion,
        string expression
        )
    {
        min ??= 0;
        bool success(int c)
        {
            return c >= min && (max is null || c <= max);
        }

        return context.AssertNotNull(
            assertion switch
            {
                MaybeNullAssertion<TItem> m => value => success(value.Count(m.Assert)),
                NotNullAssertion<TItem> n => value => success(value.Count(i => i is not null && n.Assert(i))),
                _ => throw new NotImplementedException()
            },
            expression
            );
    }
}