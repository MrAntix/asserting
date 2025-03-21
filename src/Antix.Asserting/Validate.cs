using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Antix.Asserting;

public static class Validate
{
    public static ItContext<T> It<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new() { Expression = expression!, Value = value };

    public static ItCollectionContext<TItem> It<TItem>(
        IReadOnlyCollection<TItem>? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new() { Expression = expression!, Value = value };

    public static ItCollectionContext<TItem> It<TItem>(
        TItem[]? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new() { Expression = expression!, Value = value };
}
