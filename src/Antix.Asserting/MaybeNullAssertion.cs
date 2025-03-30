using System;

namespace Antix.Asserting;

public record MaybeNullAssertion<T>(
    Func<T?, bool> Assert,
    string Error, string? ErrorNot = null
    ) : IAssertion<T>;
