using System;
using System.Runtime.CompilerServices;

namespace Antix.Asserting;

public interface IValidate<out T> : IHasErrors
{
    IValidate<T> Not { get; }

    IValidate<T> Is(
        Func<IValidate<T>, bool> tests
    );

    IValidate<T> It<TNext>(
        TNext? value,
        Func<IValidate<TNext>, bool> tests,
        [CallerArgumentExpression(nameof(value))] string? expression = null
    );

    IValidate<T> It<TNext>(
        Func<T, TNext?> getValue,
        Func<IValidate<TNext>, bool> tests,
        [CallerArgumentExpression(nameof(getValue))] string? expression = null
    );

    bool AssertMaybeNull(
        Func<T?, bool> predicate,
        string error,
        string? errorNot = null
        );
    bool AssertNotNull(
        Func<T, bool> predicate,
        string error,
        string? errorNot = null
        );
}