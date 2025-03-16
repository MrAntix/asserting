using System;

namespace Antix.Assertions;

public sealed record Assertion<T>
{
    public required Func<bool> Test { get; init; }
    public required bool Negate { get; init; }
    public required string FailMessage { get; init; }
    public string? FailNotMessage { get; init; }

    public string? Run() => Test() != Negate
            ? null
            : Negate
                ? FailNotMessage ?? $"not-{FailMessage}"
                : FailMessage;
}
