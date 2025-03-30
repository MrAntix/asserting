using System;

namespace Antix.Asserting;

public record NotNullAssertion<T>(
    Func<T, bool> Assert,
    string Error, string? ErrorNot = null
    ) : IAssertion<T>;
