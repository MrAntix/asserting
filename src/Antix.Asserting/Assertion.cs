namespace Antix.Asserting;

public abstract record Assertion
{
    public required bool Success { get; init; }
    public required string FailMessage { get; init; }
    public string? FailNotMessage { get; init; }
}

public sealed record MaybeNullAssertion : Assertion;
public sealed record NotNullAssertion : Assertion;
