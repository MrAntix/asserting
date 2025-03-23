using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Antix.Asserting;

public static class Validate
{
    public static ItemItContext<T> It<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new(value) { Expression = expression! };

    public static ItemItContext<T> ItTemp<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
    ) => new(value) { Expression = expression! };

    public static CollectionItContext<TItem> It<TItem>(
        IReadOnlyCollection<TItem>? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new(value) { Expression = expression! };

    public static CollectionItContext<TItem> It<TItem>(
        TItem[]? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new(value) { Expression = expression! };
}
