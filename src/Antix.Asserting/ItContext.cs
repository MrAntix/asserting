using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Antix.Asserting;

public abstract record ItContext<TContext, TValue>(
    TValue Value
    )
    where TContext : ItContext<TContext, TValue>
{
    public required string Expression { get; init; }
    public bool Negate { get; init; } = false;

    public ImmutableArray<string> Errors { get; init; } = [];

    public bool Failure(out ImmutableArray<string> errors) =>
        !(errors = Errors).IsEmpty;

    public bool Success => Errors.IsEmpty;

    public TContext Not => (TContext)this with { Negate = !Negate };

    public TContext Is(
        params Func<TValue, Assertion>[] providers
        ) => (TContext)this with
        {
            Errors = Errors.AddRange(
                providers
                    .Select(provider => provider.Run(Value, Negate))
                    .Where(error => error is not null)
                    .Select(error => FormatError(error!))
                ),
            Negate = false
        };

    public ItemItContext<TNext> It<TNext>(
        TNext? value,
        bool negate = false,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new(value)
        {
            Expression = expression!,
            Negate = negate,
            Errors = Errors
        };

    public CollectionItContext<TNext> It<TNext>(
        TNext[]? value,
        bool negate = false,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new(value)
        {
            Expression = expression!,
            Negate = negate,
            Errors = Errors
        };

    public CollectionItContext<TNext> It<TNext>(
        IReadOnlyCollection<TNext>? value,
        bool negate = false,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new(value)
        {
            Expression = expression!,
            Negate = negate,
            Errors = Errors
        };

    protected string FormatError(
        string error,
        string? expressionSuffix = null
        ) => $"{Expression}{expressionSuffix}:{error}";
}