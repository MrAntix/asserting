using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Antix.Assertions;

public sealed class AssertionContext<T>(
    string name, T? value,
    IEnumerable<Func<T?, Assertion<T>>> assertions,
    Func<ImmutableArray<string>>? previous = null
)
{
    readonly ImmutableArray<Func<T?, Assertion<T>>> _assertions
        = [.. assertions];

    public ImmutableArray<string> Validate() => [
        .. previous is null ? [] : previous(),
        .. _assertions
            .Select(a => a(value).Run())
            .Where(error => error is not null)
            .Select(error => $"{name}:{error}")
            ];

    public ValueContext<TValue> For<TValue>(
        TValue? value,
        [CallerArgumentExpression(nameof(value))] string? caller = null
        ) => new(caller!, value, Validate);
}
