using System;

namespace Antix.Asserting;

public sealed record Assertion
{
    public required Func<bool> TestSuccess { get; init; }
    public required string FailMessage { get; init; }
    public string? FailNotMessage { get; init; }

    public string? Run(bool negate) => TestSuccess() != negate
            ? null
            : negate
                ? FailNotMessage ?? $"not-{FailMessage}"
                : FailMessage;
}
