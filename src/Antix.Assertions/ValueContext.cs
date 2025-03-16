using System;
using System.Collections.Immutable;

namespace Antix.Assertions;

public sealed class ValueContext<T>(
    string name, T? value,
    Func<ImmutableArray<string>>? previous = null
)
{
    public AssertionContext<T> Assert(
        params Func<T?, Assertion<T>>[] assertions
        ) => new(name, value, assertions, previous);
}
