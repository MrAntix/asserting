using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Antix.Asserting;

public static class Validate
{
    public static IValidate<T> It<T>(
        T? value,
        Func<IValidate<T>, bool> tests,
        [CallerArgumentExpression(nameof(value))] string? expression = null
        ) => new Validate<T>(value, expression!, [])
            .Is(tests);

    public static bool Assert<T>(
        this IValidate<T> context,
        IAssertion<T> assertion
        ) => assertion switch
        {
            MaybeNullAssertion<T> m => context.AssertMaybeNull(m.Assert, m.Error, m.ErrorNot),
            NotNullAssertion<T> n => context.AssertNotNull(n.Assert, n.Error, n.ErrorNot),
            _ => throw new NotImplementedException()
        };

    public static bool Ok(this IHasErrors has)
        => has.Errors.Count == 0;

    public static bool HasError(this IHasErrors has, out string error)
        => (error = string.Join('\n', has.Errors)) != string.Empty;

    public static bool HasErrors(this IHasErrors has, out ImmutableArray<string> errors)
        => !(errors = [.. has.Errors]).IsEmpty;
}

public sealed record Validate<T>(
    T? Value,
    string Expression,
    List<string> Errors,
    bool Negate = false
    ) : IValidate<T>
{
    IReadOnlyList<string> IHasErrors.Errors => Errors;

    public IValidate<T> Not => new Validate<T>(
        Value, Expression, Errors, !Negate
        );

    public IValidate<T> Is(
        Func<IValidate<T>, bool> tests
        )
    {
        var context = new Validate<T>(
            Value, Expression, []
            );

        if (tests(context) == Negate)
            Errors.AddRange(context.Errors);

        return this;
    }

    public IValidate<T> It<TNext>(
        TNext? value,
        Func<IValidate<TNext>, bool> tests,
        string? expression = null
        )
    {
        var context = new Validate<TNext>(
            value, expression!, []
            );

        if (tests(context) == Negate)
            Errors.AddRange(context.Errors);

        return this;
    }

    public IValidate<T> It<TNext>(
        Func<T, TNext?> getValue,
        Func<IValidate<TNext>, bool> tests,
        string? expression = null
        ) => Value is not null
            ? It(getValue(Value), tests, $"{Expression}.{expression}")
            : this;

    public bool AssertMaybeNull(
        Func<T?, bool> predicate,
        string failMessage, string? failNotMessage = null
        ) => Test(predicate(Value), failMessage, failNotMessage);

    public bool AssertNotNull(
        Func<T, bool> predicate,
        string failMessage, string? failNotMessage = null
        ) => Value is null || Test(predicate(Value), failMessage, failNotMessage);

    bool Test(
        bool success,
        string failMessage,
        string? failNotMessage = null
    )
    {
        if (success != Negate) return true;

        var message = Negate
            ? failNotMessage ?? $"not-{failMessage}"
            : failMessage;

        Errors.Add($"{Expression}:{message}");

        return false;
    }
}
