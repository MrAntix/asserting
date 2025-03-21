using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Antix.Asserting;

public abstract record ItContextBase
{
    public required string Expression { get; init; }
    public bool Negate { get; init; } = false;

    protected ImmutableArray<Func<string?>> _assertions = [];

    public ImmutableArray<string> Run() =>
        [.. _assertions
            .Select(assertion => assertion())
            .Where(error => error is not null)!
        ];

    public bool Fails(out ImmutableArray<string> errors) =>
        !(errors = Run()).IsEmpty;

    public ItContext<TNext> It<TNext>(
        TNext? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new()
        {
            Expression = expression!,
            Value = value,
            _assertions = _assertions
        };

    public ItCollectionContext<TNext> It<TNext>(
        TNext[]? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new()
        {
            Expression = expression!,
            Value = value,
            _assertions = _assertions
        };

    public ItCollectionContext<TNext> It<TNext>(
        IReadOnlyCollection<TNext>? value,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new()
        {
            Expression = expression!,
            Value = value,
            _assertions = _assertions
        };

}